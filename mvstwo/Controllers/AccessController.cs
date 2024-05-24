using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mvstwo.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks.Dataflow;

namespace mvstwo.Controllers
{
    public class AccessController : Controller
    {
        OkeiSiteContext db;
        public AccessController(OkeiSiteContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated) return RedirectToAction("Main", "CokMain");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            
            if(db.Users.FirstOrDefault(q=>q.Login==user.Login && q.Password == user.Password) != null)
            {
                user = db.Users.FirstOrDefault(q => q.Login == user.Login && q.Password == user.Password);
                Group group = db.Groups.FirstOrDefault(x => x.Id == user.Usergroup);
                Role role = db.Roles.FirstOrDefault(x => x.Id == user.RoleUser);
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Login),
                    new Claim("Role", role.Name),
                    new Claim("IO", user.Surename + " " + user.Firstname),
                    new Claim("Group", group.Name)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Main", "CokMain");
            
            }
            
            ViewData["ValidateMessage"] = "Неверный логин/пароль";
            return View();
        }
    }
}
