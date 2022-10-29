using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_4a.Data;
using Lab_4a.Models;
using Microsoft.AspNetCore.Authorization;
using Lab_4a.Data.Dao;
using System.Security.Claims;

namespace Lab_4a.Controllers
{
    [Authorize]

    public class _TaskController : Controller
    {
        private readonly ITaskDao _dao;

        public _TaskController(IAtlasSettings settings)
        {
            _dao = new TaskDao(settings);
        }

        // GET: _Task
        public async Task<IActionResult> Index()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's UserId
            ViewData["UserId"] = UserId;

            IEnumerable<_Task> tasks = await _dao.Read(UserId);
            ViewData["tasks"] = tasks;

            return View();
        }
        

        // POST: _Task/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,text,date")] _Task _Task)
        {
            if (ModelState.IsValid)
            {
                await _dao.Create(_Task);
                return RedirectToAction(nameof(Index));
            }
            return View(_Task);
        }

        // POST: _Task/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,date,text,done")] _Task _Task)
        {
            if (id != _Task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Task.done = !_Task.done;
                    await _dao.Update(_Task);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_Task);
        }

        // POST: _Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _dao.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
