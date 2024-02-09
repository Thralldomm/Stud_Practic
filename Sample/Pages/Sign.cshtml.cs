using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using Sample.Models;
using Sample.Application;
using Sample.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace SampleApp.RazorPage.Pages
{
    public class SignModel : PageModel
    {
        private readonly MasterContext _db;
        private IFlasher _f; 
        public SignModel(MasterContext db, IFlasher f)
        {
            _db = db;
            _f = f; 
        }
        public IActionResult YourAction()
        {
            _f.Flash(Types.Success, "Flash message system for ASP.NET MVC Core", dismissable: true);
            _f.Flash(Types.Danger, "Flash message system for ASP.NET MVC Core", dismissable: false);
            return RedirectToAction("AnotherAction");
        }
         

        //async
        public IActionResult OnPost(User user)
        {
        
            try
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                _f.Flash(Types.Success, $"Пользователь  {user.Name} зарегистрирован!", dismissable: true);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                return RedirectToPage("./Sign");
            }
        }

    }

}