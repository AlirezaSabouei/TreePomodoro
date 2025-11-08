using Application.Common.Data;
using Application.Glasses.Queries;
using Domain.Entities.Glasses;
using MediatR;

namespace Application.Glasses.Commands;

public record DeleteGlassCommand : IRequest
{
    public Guid UserId { get; set; }
}

public class DeleteGlassCommandHandler(
    IRequestHandler<GetGlassesQuery, List<Glass>> getGlassesQueryHandler,
    IDocumentStore<Glass> documentStore) : IRequestHandler<DeleteGlassCommand>
{
    public async Task Handle(DeleteGlassCommand request, CancellationToken cancellationToken)
    {
        var glasses = await GetGlassesAsync(request,cancellationToken);
        if (glasses.Count > 0)
        {
            var glass = glasses.First();
            await documentStore.DeleteAsync(glass.Id);
        }
    }

    private async Task<List<Glass>> GetGlassesAsync(DeleteGlassCommand request, CancellationToken cancellationToken)
    {
        var query = new GetGlassesQuery()
        {
            UserId = request.UserId,
            Today = DateTime.Today
        };
        return await getGlassesQueryHandler.Handle(query, cancellationToken);
    }
}