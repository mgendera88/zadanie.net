using Microsoft.EntityFrameworkCore;
using System.Net.Security;
using zadanie_rekrutacyjne;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:80");
var con = builder.Configuration.GetConnectionString("TodoDb");
Console.WriteLine($"Connection string: {con}");
builder.Services.AddDbContext<ToDoBaza>(options => options.UseNpgsql(con));
builder.Services.AddControllersWithViews();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
//GET Endpoint
app.MapGet("/todos", async (ToDoBaza db) =>
    await db.Todos.ToListAsync());
//GET Endpoint for specific Todo
app.MapGet("/todos/{id}", async (int id, ToDoBaza db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound());
//POST Endpoint
app.MapPost("/todos", async (Todo todo, ToDoBaza db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", todo);
});
//PUT Endpoint
app.MapPut("/todos/{id}", async (int id, Todo inputTodo, ToDoBaza db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo == null)
    {
        return Results.NotFound();
    }
    //Ensures that the DateTime is specified, caused errrors
    if(inputTodo.DueTime.Kind == DateTimeKind.Unspecified)
    {
        inputTodo.DueTime = DateTime.SpecifyKind(inputTodo.DueTime, DateTimeKind.Utc);
    }
    todo.Title = inputTodo.Title;
    todo.Description = inputTodo.Description;
    todo.DueTime = inputTodo.DueTime;
    todo.isComplete = inputTodo.isComplete;
    todo.percent_done = inputTodo.percent_done;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
//DELETE Endpoint
app.MapDelete("/todos/{id}", async (int id, ToDoBaza db) =>
{
    if(await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});
HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) =>
{
    return true;
};
HttpClient client = new HttpClient(clientHandler);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
