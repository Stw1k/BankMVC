using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankDomain.Model;
using BankInfastructure;

namespace BankInfastructure.Controllers
{
    public class CurrenciesController : Controller
    {
        private readonly BankDBContext _context;

        public CurrenciesController(BankDBContext context)
        {
            _context = context;
        }

        // GET: Currencies
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Currencys","Index");
            ViewBag.BankId = id;
            ViewBag.BankName = name;
            var CurrencyByBank = _context.Currencys.Where(b => b.BankId == id).Include(b => b.Bank);
            //var bankDBContext = _context.Currencys.Include(c => c.Bank);
            //return View(await bankDBContext.ToListAsync());
            return View(await CurrencyByBank.ToListAsync());
        }

        // GET: Currencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencys
                .Include(c => c.Bank)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
            
        }

        // GET: Currencies/Create
        public IActionResult Create(int bankId)
        {
            //ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name");
            ViewBag.BankId = bankId;
            ViewBag.BankName = _context.Banks.Where(b => b.Id == bankId).FirstOrDefault().Name;
            return View();
        }

        // POST: Currencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int  bankId, [Bind("CurrencyName,BankId,CurrencyRate,Id")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(currency);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index","Currencies", new {id = bankId, name = _context.Banks.Where(b => b.Id == bankId).FirstOrDefault().Name});
            }
            //ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", currency.BankId);
            //return View(currency);
            return RedirectToAction("Index", "Currencies", new { id = bankId, name = _context.Banks.Where(b => b.Id == bankId).FirstOrDefault().Name });
        }

        // GET: Currencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencys.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", currency.BankId);
            return View(currency);
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurrencyName,BankId,CurrencyRate,Id")] Currency currency)
        {
            if (id != currency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(currency.Id))
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
            ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", currency.BankId);
            return View(currency);
        }

        // GET: Currencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencys
                .Include(c => c.Bank)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currency = await _context.Currencys.FindAsync(id);
            if (currency != null)
            {
                _context.Currencys.Remove(currency);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyExists(int id)
        {
            return _context.Currencys.Any(e => e.Id == id);
        }
    }
}
