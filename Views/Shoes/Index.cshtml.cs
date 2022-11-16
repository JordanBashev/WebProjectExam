using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebProjectExam.Database;
using WebProjectExam.Models.ViewModels;

namespace WebProjectExam.Views
{
    public class IndexModel : PageModel
    {
        private readonly WebProjectExam.Database.ShoeStoreDbContext _context;

        public IndexModel(WebProjectExam.Database.ShoeStoreDbContext context)
        {
            _context = context;
        }

        public IList<ShoeVM> ShoeVM { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ShoeVM != null)
            {
                ShoeVM = await _context.ShoeVM.ToListAsync();
            }
        }
    }
}
