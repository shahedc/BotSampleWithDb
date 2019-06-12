﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EchoBotWithDb.Data;
using EchoBotWithDb.Models;

namespace EchoBotWithDb.Controllers
{
    public class HumanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HumanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Human
        public async Task<IActionResult> Index()
        {
            return View(await _context.Human.ToListAsync());
        }

        // GET: Human/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var human = await _context.Human
                .FirstOrDefaultAsync(m => m.Id == id);
            if (human == null)
            {
                return NotFound();
            }

            return View(human);
        }

        // GET: Human/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Human/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Title")] Human human)
        {
            if (ModelState.IsValid)
            {
                _context.Add(human);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(human);
        }

        // GET: Human/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var human = await _context.Human.FindAsync(id);
            if (human == null)
            {
                return NotFound();
            }
            return View(human);
        }

        // POST: Human/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Title")] Human human)
        {
            if (id != human.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(human);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HumanExists(human.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(human);
        }

        // GET: Human/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var human = await _context.Human
                .FirstOrDefaultAsync(m => m.Id == id);
            if (human == null)
            {
                return NotFound();
            }

            return View(human);
        }

        // POST: Human/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var human = await _context.Human.FindAsync(id);
            _context.Human.Remove(human);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HumanExists(int id)
        {
            return _context.Human.Any(e => e.Id == id);
        }
    }
}
