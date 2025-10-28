using Domain.Entities.Gardens;

namespace Application.Gardens;

public interface IGardenClient
{
    Task TreeCompleted(Guid gardenId, TreeState newState);
}