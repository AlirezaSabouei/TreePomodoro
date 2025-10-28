using Microsoft.AspNetCore.SignalR;

namespace Application.Gardens;

public class GardenHub : Hub<IGardenClient>
{
    public override async Task OnConnectedAsync()
    {
        var gardenId = Context.GetHttpContext()?.Request.Query["gardenId"];
        if (!string.IsNullOrEmpty(gardenId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gardenId);
        }

        await base.OnConnectedAsync();
    }
}