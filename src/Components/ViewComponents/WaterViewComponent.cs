using Application;
using Application.Glasses.Commands;
using Application.Glasses.Queries;
using Components.Models;
using Domain.Entities.Glasses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Components.ViewComponents;

public class WaterViewComponent(
    IRequestHandler<GetGlassesQuery, List<Glass>> getGlassesQueryHandler,
    IRequestHandler<CreateGlassCommand, Glass> createGlassCommandHandler,
    SignedUser signedUser) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(RequestType requestType)
    {
        if (requestType == RequestType.Add)
        {
            await AddGlassAsync();
        }

        var glasses = await GetGlassesAsync();
        
        return View("Index", glasses);
    }

    private async Task AddGlassAsync()
    {
        var command = new CreateGlassCommand()
        {
            UserId = signedUser.UserId
        };
        await createGlassCommandHandler.Handle(command, CancellationToken.None);
    }
    
    private async Task<List<Glass>> GetGlassesAsync()
    {
        var query = new GetGlassesQuery()
        {
            Today = DateTime.Today,
            UserId = signedUser.UserId
        };
        return await getGlassesQueryHandler.Handle(query, CancellationToken.None);
    }
}