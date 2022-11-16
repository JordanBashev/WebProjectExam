using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProjectExam.Database;
using WebProjectExam.Models.ViewModels;

namespace WebProjectExam.Views
{
    public class CreateModel : PageModel
    {
        private readonly WebProjectExam.Database.ShoeStoreDbContext _context;

        public CreateModel(WebProjectExam.Database.ShoeStoreDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ShoeVM ShoeVM { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ShoeVM.Add(ShoeVM);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
