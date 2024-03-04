namespace ObligationStation.Services;

public interface IStateService
{
    UserDto GetCreateUser(string userName);
    UserDto GetUser(string userName);
    UserDto EditUser(UserDto userDto);
    TodoDto GetTodo(string userName, Guid id);
    TodoDto CreateTodo(string userName, string description);
    TodoDto EditTodo(string userName, TodoDto todoDto);
    void RemoveTodo(string userName, Guid id);
}