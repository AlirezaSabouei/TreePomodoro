using Microsoft.EntityFrameworkCore;
using Project.Business.Common.Data;
using Project.Domain.Entities.Gardens;


namespace Project.Business.Services.Gardens;

public class GardenServices(Context context, SignedUser signedUser) //: BaseService<Garden>(context)
{
    public async Task<Garden> GetOrCreateGarden()
    {
        var garden = await GetGardenAsync() ?? await CreateGardenAsync();
        return garden;
    }

    private async Task<Garden?> GetGardenAsync()
    {
        var gardens = await context.Gardens
            .Where(a => a.UserId == signedUser.UserId)
            .Include(a => a.Trees)
            .ToListAsync();
        var garden = gardens.FirstOrDefault(a => a.Date == DateTime.Today);
        garden?.RefreshTrees();
        return garden;
    }

    private async Task<Garden> CreateGardenAsync()
    {
        var garden = new Garden()
        {
            UserId = signedUser.UserId,
            Date = DateTime.Today
        };
        await context.Gardens.AddAsync(garden);
        await context.SaveChangesAsync();
        return garden;
        // var gardens = await context.Gardens
        //     .Where(a => a.UserId == signedUser.UserId)
        //     .ToListAsync();
        // return gardens.First(a => a.Date == garden.Date);
    }

    public async Task<Garden> SpawnATree()
    {
        var garden = await GetGardenAsync();
        garden!.SpawnATree();
        await context.AddAsync(garden.Trees.Last());
        await context.SaveChangesAsync();
        return garden;
    }
}