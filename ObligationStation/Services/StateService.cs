using Microsoft.AspNetCore.Mvc;
using ObligationStation.Entities;

namespace ObligationStation.Services;

public class StateService : IStateService
{
    private readonly Dictionary<string, User> _users = [];

    public UserDto GetCreateUser(string userName)
    {
        if (_users.TryGetValue(userName, out var user)) return new UserDto(user);

        user = new User(userName);
        _users.Add(userName, user);

        return new UserDto(user);
    }

    [HttpGet]
    public UserDto GetUser(string userName)
    {
        if (!_users.TryGetValue(userName, out var user))
        {
            throw new KeyNotFoundException("User does not exist");
        }
            
        return new UserDto(user);
    }

    public UserDto EditUser(UserDto userDto)
    {
        if (!_users.TryGetValue(userDto.UserName, out _))
        {
            throw new KeyNotFoundException("User does not exist");
        }

        var newUser = new User(userDto.UserName)
        {
            ProfilePicture = userDto.ProfilePicture,
            Todos = userDto.Todos.Select(x => new Todo(x.Description!)
            {
                Id = x.Id,
                Status = (TodoStatus)x.Status
            }).ToDictionary(x => x.Id)
        };

        _users[userDto.UserName] = newUser;

        return new UserDto(newUser);
    }

    public TodoDto GetTodo(string userName, Guid id)
    {
        if (!_users.TryGetValue(userName, out var user))
        {
            throw new KeyNotFoundException("User does not exist");
        }
        
        if (!user.Todos.TryGetValue(id, out var todo))
        {
            throw new KeyNotFoundException("Todo does not exist");
        }

        return new TodoDto(todo);
    }

    public TodoDto CreateTodo(string userName, string description)
    {
        if (!_users.TryGetValue(userName, out var user))
        {
            throw new KeyNotFoundException("User does not exist");
        }

        var newTodo = new Todo(description);
        user.Todos.Add(newTodo.Id, newTodo);

        return new TodoDto(newTodo);
    }

    public TodoDto EditTodo(string userName, TodoDto todoDto)
    {
        if (!_users.TryGetValue(userName, out var user))
        {
            throw new KeyNotFoundException("User does not exist");
        }

        if (!user.Todos.TryGetValue(todoDto.Id, out _))
        {
            throw new KeyNotFoundException("Todo does not exist");
        }

        var newTodo = new Todo(todoDto.Description!)
        {
            Id = todoDto.Id,
            Status = (TodoStatus)todoDto.Status
        };

        user.Todos[todoDto.Id] = newTodo;

        return new TodoDto(newTodo);
    }

    public void RemoveTodo(string userName, Guid id)
    {
        if (!_users.TryGetValue(userName, out var user))
        {
            throw new KeyNotFoundException("User does not exist");
        }

        if (!user.Todos.Remove(id))
        {
            throw new KeyNotFoundException("Todo does not exist");
        }
    }
}