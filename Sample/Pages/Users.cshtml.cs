using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sample.Models;

namespace Sample.Pages
{
    public class UsersModel : PageModel
    {
        public void OnGet()
        {
        }
        private readonly MasterContext _db;
        public UsersModel(MasterContext db)
        {
            _db = db;
        }

        public IList<User> Users { get; set; }
        public User User { get; set; }
        public string sessionId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _db.Users.ToListAsync();
            return Page();
        }
    }
}
