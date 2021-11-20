using Microsoft.EntityFrameworkCore;
using MiniTodo.Data;
using MiniTodo.ViewModels;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/v1/todos", async (AppDbContext context) =>
{
    var todos = await context.Todos.ToListAsync();
    return todos is not null ? Results.Ok(todos) : Results.NotFound();
}).Produces<Todo>();


app.MapGet("/v1/todos/{id}", async (Guid id, AppDbContext context) =>
{
    var todo = await context.Todos.FindAsync(id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
}).Produces<Todo>();


app.MapPost("/v1/todos", (AppDbContext context, CreateTodoViewModel model) =>
{
    var todo = model.MapTo();
    if (!model.IsValid) return Results.BadRequest(model.Notifications);
    context.Todos.Add(todo);
    context.SaveChanges();
    return Results.Created($"/v1/todos/{todo.Id}", todo);
});


app.MapPut("/todos/{id}", async (Guid id, UpdateTodoViewModel model, AppDbContext context) =>
{
    var todo = await context.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();
    model.MapTo(todo);
    if (!model.IsValid) return Results.BadRequest(model.Notifications);
    await context.SaveChangesAsync();
    return Results.NoContent();
});


app.MapDelete("/todos/{id}", async (Guid id, AppDbContext context) =>
{
    var todo = await context.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();
    context.Todos.Remove(todo);
    await context.SaveChangesAsync();
    return Results.Ok(todo);
});


app.Run();
