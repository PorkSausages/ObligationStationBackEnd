using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ObligationStation.Services;

namespace ObligationStation.Controllers;

[ApiController]
[Route("api/[controller]/[action]/{userName}")]
public class AppController(IStateService stateService) : ControllerBase
{
    [HttpGet]
    public ActionResult<UserDto> GetCreateUser(string userName)
    {
        return Ok(stateService.GetCreateUser(userName));
    }
    
    [HttpGet]
    public ActionResult<UserDto> GetUser(string userName)
    {
        try
        {
            return Ok(stateService.GetUser(userName));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPatch]
    public ActionResult<UserDto> UpdateUser(string userName, [FromBody] JsonPatchDocument<UserDto> userDocument)
    {
        try
        {
            var userDto = stateService.GetUser(userName);
            userDocument.ApplyTo(userDto);
            return stateService.EditUser(userDto);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (JsonPatchException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    public ActionResult<TodoDto> CreateTodo(string userName, [FromBody] string description)
    {
        try
        {
            return stateService.CreateTodo(userName, description);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [Route("{id:guid}")]
    [HttpPatch]
    public ActionResult<TodoDto> UpdateTodo(
        string userName, 
        Guid id,
        [FromBody] JsonPatchDocument<TodoDto> todoDocument)
    {
        try
        {
            var todoDto = stateService.GetTodo(userName, id);
            todoDocument.ApplyTo(todoDto);
            return stateService.EditTodo(userName, todoDto);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (JsonPatchException e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("{id:guid}")]
    [HttpDelete]
    public ActionResult RemoveTodo(string userName, Guid id)
    {
        try
        {
            stateService.RemoveTodo(userName, id);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}