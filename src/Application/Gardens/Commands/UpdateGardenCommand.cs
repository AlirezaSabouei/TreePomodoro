using Application.Common.Data;
using Domain.Entities.Gardens;
using MediatR;

namespace Application.Gardens.Commands;

public record UpdateGardenCommand : IRequest<Garden>
{
    public required Garden Garden { get; set; }
}

public class UpdateGardenCommandHandler(
    IDocumentStore<Garden> documentStore)
    : IRequestHandler<UpdateGardenCommand, Garden>
{
    public async Task<Garden> Handle(UpdateGardenCommand request, CancellationToken cancellationToken)
    {
        await documentStore.UpdateAsync(request.Garden);
        return request.Garden;
    }
}