using ObligationStation.Entities;

namespace ObligationStation.Services;

public class TodoDto(Todo todo)
{
    public Guid Id { get; } = todo.Id;
    public string? Description { get; set;  } = todo.Description;
    public int Status { get; set;  } = (int)todo.Status;
}