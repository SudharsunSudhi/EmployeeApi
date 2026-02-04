using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;


    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        return Ok(await _context.Employees.ToListAsync());
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        return emp == null ? NotFound() : Ok(emp);
    }


    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return Ok(employee);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee employee)
    {
        if (id != employee.Id) return BadRequest();
        _context.Entry(employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(employee);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        if (emp == null) return NotFound();
        _context.Employees.Remove(emp);
        await _context.SaveChangesAsync();
        return Ok();

    }

    //testing
    public void SayHello()
    {
        Console.WriteLine("Hello, welcome!");
    }


}
