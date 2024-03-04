using ObligationStation.Entities;

namespace ObligationStation.Services;

public class UserDto(User user)
{
    public string UserName { get; } = user.UserName;
    public string? ProfilePicture { get; init; } = user.ProfilePicture; 
    public List<TodoDto> Todos { get; } = user.Todos.Select(x => new TodoDto(x.Value)).ToList();
}