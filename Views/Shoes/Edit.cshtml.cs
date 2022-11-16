using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProjectExam.Database;
using WebProjectExam.Models.ViewModels;

namespace WebProjectExam.Views
{
    public class EditModel : PageModel
    {
        private readonly WebProjectExam.Database.ShoeStoreDbContext _context;

        public EditModel(WebProjectExam.Database.ShoeStoreDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ShoeVM ShoeVM { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ShoeVM == null)
            {
                return NotFound();
            }

            var shoevm =  await _context.ShoeVM.FirstOrDefaultAsync(m => m.Id == id);
            if (shoevm == null)
            {
                return NotFound();
            }
            ShoeVM = shoevm;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ShoeVM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoeVMExists(ShoeVM.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ShoeVMExists(int id)
        {
          return _context.ShoeVM.Any(e => e.Id == id);
        }
    }
}
