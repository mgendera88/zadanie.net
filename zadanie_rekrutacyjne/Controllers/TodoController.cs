using Microsoft.AspNetCore.Mvc;
using zadanie_rekrutacyjne.Models;
using Microsoft.EntityFrameworkCore;

namespace zadanie_rekrutacyjne.Controllers
{
    public class TodoController : Controller
    {
        private readonly ToDoBaza _db;
        public TodoController(ToDoBaza db)
        {
            _db = db;
        }
        public async Task<IActionResult> TodosList()
        {
            var todos = await _db.Todos.ToListAsync();
            var todostoday = todos.Where(t=>t.DueTime == DateTime.Today).ToList();
            var tomorrow = DateTime.Today.AddDays(1);
            var todostomorrow = todos.Where(t=>t.DueTime==tomorrow).ToList();
            var todosweek = todos.Where(t => t.DueTime > DateTime.Today && t.DueTime <= DateTime.Today.AddDays(7));
            ViewBag.Today = todostoday;
            ViewBag.Tomorrow = todostomorrow;
            ViewBag.Week = todosweek;
            return View("TodosList",todos);
        }
        [HttpGet]
        public async Task<IActionResult> CreateTodo()
        {
            return View("CreateTodo");
        }
        [HttpPost]
        public async Task<IActionResult> AddTodo(Todo newtodo)
        {
            newtodo.DueTime = DateTime.SpecifyKind(newtodo.DueTime, DateTimeKind.Utc);
            newtodo.Id = 0;
            if (ModelState.IsValid)
            {
                _db.Todos.Add(newtodo);
                await _db.SaveChangesAsync();
                return RedirectToAction("TodosList");   
            }
            return View("CreateTodo",newtodo);
        }
    }
}
