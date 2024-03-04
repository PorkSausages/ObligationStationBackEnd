namespace ObligationStation.Entities;

public class Todo(string description)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string? Description { get; init; } = description;
    public TodoStatus Status { get; init; }
}

public enum TodoStatus
{
    Pending,
    InProgress,
    Completed
}