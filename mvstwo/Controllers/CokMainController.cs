using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvstwo;
using mvstwo.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace mvstwo.Controllers
{
    [Authorize]
    public class CokMainController : Controller
    {
        OkeiSiteContext db;
        public CokMainController(OkeiSiteContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(db);
        }
        [HttpPost]
        public async Task<IActionResult> Cok(int IdCok)
        {
            ViewBag.IdCok = IdCok;
            return View(db);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
        public async Task<IActionResult> Place(int IdCok)
        {
			ViewBag.IdCok = IdCok;
			return View(db);
        }
        public async Task<IActionResult> Main()
        {
            var Group = HttpContext.User.FindFirst("Group");
            var Logins = HttpContext.User.FindFirst("IO");
            ViewBag.rar = Logins.Value;
            ViewBag.ror = Group.Value;
            return View(db);
        }
        [Authorize(Policy = "Teacher")]
        public async Task<IActionResult> ProfileTeacher()
        {
            var Logins = HttpContext.User.FindFirst("IO");
            ViewBag.rar = Logins.Value;
            return View(db);
        }
        
        public async Task<IActionResult> Quiz(int IdCok, int nowquiz, int score, int timeMinute, int timeSecond)
        {
            ViewBag.nowPage = nowquiz;
            ViewBag.IdCok = IdCok;
            ViewBag.score = score;
            ViewBag.minute = timeMinute;
            ViewBag.second = timeSecond;
            return View(db);
        }
        public async Task<IActionResult> Result(int IdCok, int nowquiz)
        {
            ViewBag.nowPage = nowquiz;
            ViewBag.IdCok = IdCok;
            return View(db);
        }//
        public async Task<IActionResult> Edit1()
        {
            if (1 != null)
            {
                Cok? cok = await db.Coks.FirstOrDefaultAsync(p => p.Id == 2);
                if (cok != null) return View(cok);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddCok(Cok a)
        {
            db.Coks.Add(a);
            await db.SaveChangesAsync();
            return RedirectToAction("Main");
        }
        public async Task<IActionResult> AddCok()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User a)
        {
            db.Users.Add(a);
            await db.SaveChangesAsync();
            return RedirectToAction("Main");
        }
        
        public async Task<IActionResult> AddUser()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int IdCok)
        {
            ViewBag.f = IdCok;
            if (1 != null)
            {
                Model.OkeiSiteContext cokContext = new OkeiSiteContext();
                if (cokContext != null) return View(cokContext);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Quest quest)
        {
                // Обновляем информацию о вопросе
                db.Quests.Update(quest);

                // Сохраняем все изменения в базе данных
                await db.SaveChangesAsync();

                // Возвращаем успешный результат в формате JSON
                return Json(new { success = true });
        }
        [HttpGet]
        public async Task<IActionResult> EditCokPage(int IdCok)
        {
            ViewBag.f = IdCok;
            if (1 != null)
            {
                Model.OkeiSiteContext cokContext = new OkeiSiteContext();
                if (cokContext != null) return View(cokContext);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditCok(Cok coks)
        {
            db.Coks.Update(coks);
            await db.SaveChangesAsync();
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> EditEom(Eom eom1)
        {
            db.Eoms.Update(eom1);
            
            await db.SaveChangesAsync();
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> Edit2(Answer answer)
        {
            db.Answers.Update(answer);

            await db.SaveChangesAsync();
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> EditAccord(TestAccordBlock testAccord)
        {
            db.TestAccordBlocks.Update(testAccord);

            await db.SaveChangesAsync();
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> EditSequence(TestSequenceBlock testSequence)
        {
            db.TestSequenceBlocks.Update(testSequence);

            await db.SaveChangesAsync();
            return Json(new { success = true });
        }


















        //public async Task<IActionResult> EditCok(int? IdCok)
        //{
        //    if (1 != null)
        //    {
        //        Cok? cok = await db.Coks.FirstOrDefaultAsync(p => p.Id == IdCok);
        //        if (cok != null) return RedirectToAction("Edit", 3);
        //    }
        //    return NotFound();
        //}
    }
}
