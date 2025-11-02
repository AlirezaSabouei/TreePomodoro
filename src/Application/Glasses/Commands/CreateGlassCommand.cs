using Application.Common.Data;
using Domain.Entities.Glasses;
using MediatR;

namespace Application.Glasses.Commands;

public record CreateGlassCommand : IRequest<Glass>
{
    public Guid UserId { get; set; }
}

public class CreateGlassCommandHandler(IDocumentStore<Glass> documentStore) : IRequestHandler<CreateGlassCommand, Glass>
{
    public async Task<Glass> Handle(CreateGlassCommand request, CancellationToken cancellationToken)
    {
        var glass = CreateGlass(request);
        await documentStore.InsertAsync(glass);
        return glass;
    }

    private Glass CreateGlass(CreateGlassCommand request)
    {
        var glass = new Glass()
        {
            UserId = request.UserId,
            Year = DateTime.Today.Year,
            Month = DateTime.Today.Month,
            Day = DateTime.Today.Day,
        };
        return glass;
    }
}