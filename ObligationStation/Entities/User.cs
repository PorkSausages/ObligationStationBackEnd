namespace ObligationStation.Entities;

public class User(string userName)
{
    public string UserName { get; } = userName;
    public string? ProfilePicture { get; set; }
    public Dictionary<Guid, Todo> Todos { get; init; } = [];
}