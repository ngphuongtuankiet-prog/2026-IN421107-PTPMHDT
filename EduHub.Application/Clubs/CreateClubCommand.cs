
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Application.Clubs;

public record CreateClubCommand(string Name, string Description);

public class CreateClubCommandHandler
{
    private const int MaxNameLength = 50;
    private readonly IClubRepository _repository;

    public CreateClubCommandHandler(IClubRepository repository)
    {
        _repository = repository;
    }

    public async Task<Club> HandleAsync(
        CreateClubCommand command,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new ArgumentException("Club name cannot be empty.");
        }

        if (command.Name.Length > MaxNameLength)
        {
            throw new ArgumentException(
                "Club name cannot exceed 50 characters.");
        }

        var club = Club.Create(command.Name, command.Description);

        await _repository.AddAsync(club, ct);

        return club;
    }
}
