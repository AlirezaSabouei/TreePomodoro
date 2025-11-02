using Application;
using Application.Gardens.Commands;
using Domain.Entities.Gardens;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Views.Shared.Components.GardenComponent;

public class GardenComponent(
    IRequestHandler<CreateGardenCommand, Garden> createGardenCommandHandler,
    IRequestHandler<CreateTreeCommand, Garden> createTreeCommandHandler,
    IRequestHandler<CompleteTreeCommand, Garden> completeTreeCommandHandler,
    SignedUser signedUser) : ViewComponent
{
    private Garden _garden = new();
    
    public async Task<IViewComponentResult> InvokeAsync(RequestType requestType)
    {
        await CreateGardenIfNecessaryAsync();
        switch (requestType)
        {
            case RequestType.Add:
                await CreateTreeAsync();
                break;
            case RequestType.Remove:
                await KillTreeAsync();
                break;
        }
        return View("GardenComponentView", _garden);
    }

    private async Task CreateGardenIfNecessaryAsync()
    {
        var command = new CreateGardenCommand()
        {
            UserId = signedUser.UserId
        };
        _garden = await createGardenCommandHandler.Handle(command, CancellationToken.None);
    }

    private async Task CreateTreeAsync()
    {
        var command = new CreateTreeCommand()
        {
            GardenId = _garden.Id,
            GrowthTimeInSeconds = signedUser.TreeGrowthTimeInSeconds
        };
        _garden =  await createTreeCommandHandler.Handle(command, CancellationToken.None);
    }

    private async Task KillTreeAsync()
    {
        var command = new CompleteTreeCommand()
        {
            GardenId = _garden.Id,
            TreeState = TreeState.Dry
        };
        _garden = await completeTreeCommandHandler.Handle(command, CancellationToken.None);
    }
}