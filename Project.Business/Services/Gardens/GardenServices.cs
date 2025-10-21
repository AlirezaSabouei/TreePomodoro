using Microsoft.EntityFrameworkCore;
using Project.Business.Common.Data;
using Project.Domain.Entities.Gardens;

namespace Project.Business.Services.Gardens;

public class GardenServices(Context context, SignedUser signedUser) //: BaseService<Garden>(context)
{
    public Garden Garden { get; set; }

    public async Task GetOrCreateGardenAsync()
    {
        Garden = await GetGardenAsync() ?? await CreateGardenAsync();
    }

    private async Task<Garden?> GetGardenAsync()
    {
        var today = DateTime.Today;
        var garden = await context.Gardens
            .Where(a => a.UserId == signedUser.UserId)
            .Where(a=>a.Year == today.Year && a.Month == today.Month && a.Day == today.Day)
            .Include(a => a.Trees)
            .FirstOrDefaultAsync();
        garden?.RefreshGarden();
        return garden;
    }

    private async Task<Garden> CreateGardenAsync()
    {
        var today = DateTime.Today;
        var garden = new Garden()
        {
            UserId = signedUser.UserId,
            Year = today.Year,
            Month = today.Month,
            Day = today.Day
        };
        await context.Gardens.AddAsync(garden);
        await context.SaveChangesAsync();
        return garden;
    }

    public async Task PlantASeedAsync()
    {
        var seed = Garden!.AddASeed();
        if (seed != null)
        {
            await context.AddAsync(seed);
            await context.SaveChangesAsync();
        }
    }
    
    public async Task KillATreeAsync()
    {
        var treeToKill = Garden!.Trees.First(a => a.TreeState == TreeState.Seed);
        treeToKill.TreeState = TreeState.Dry;
        await context.SaveChangesAsync();
    }
}