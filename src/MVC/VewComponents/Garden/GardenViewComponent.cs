using Microsoft.AspNetCore.Mvc;
using Application.Gardens.Commands;
using MediatR;
using MVC.Models;

namespace MVC.VewComponents.Garden;

public class GardenViewComponent(
    IRequestHandler<CreateGardenCommand, Domain.Entities.Gardens.Garden> createGardenCommandHandler,
    IRequestHandler<CreateTreeCommand,Domain.Entities.Gardens.Garden> createTreeCommandHandler,
    IRequestHandler<KillTreeCommand, Domain.Entities.Gardens.Garden> killTreeCommandHandler) : ViewComponent
{
    private Domain.Entities.Gardens.Garden _garden = new();
    
    public async Task<IViewComponentResult> InvokeAsync(RequestType requestType)
    {
        await CreateGardenIfNecessaryAsync();
        switch (requestType)
        {
            case RequestType.PlantASeed:
                await CreateTreeAsync();
                break;
            case RequestType.KillTheSeed:
                await KillTreeAsync();
                break;
        }
        return View("Garden", _garden);
    }

    private async Task CreateGardenIfNecessaryAsync()
    {
        var command = new CreateGardenCommand();
        _garden = await createGardenCommandHandler.Handle(command, CancellationToken.None);
    }

    private async Task CreateTreeAsync()
    {
        var command = new CreateTreeCommand();
        _garden =  await createTreeCommandHandler.Handle(command, CancellationToken.None);
    }

    private async Task KillTreeAsync()
    {
        var command = new KillTreeCommand();
        _garden = await killTreeCommandHandler.Handle(command, CancellationToken.None);
    }
}