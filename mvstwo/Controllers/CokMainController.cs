using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvstwo;
using mvstwo.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using mvstwo.FileUploadService;

namespace mvstwo.Controllers
{
    [Authorize]
    public class CokMainController : Controller
    {
        OkeiSiteContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public string FileName;
        public CokMainController(OkeiSiteContext context, IWebHostEnvironment webHostEnvironment)
        {
            db = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> vost()
        {
            
            return RedirectToAction("Main");
        }
        public async Task<IActionResult> back()
        {
            return RedirectToAction("Main");
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
            int eom = db.Eoms.FirstOrDefault(q=>q.Id == db.Contents.FirstOrDefault
                        (q => q.IdCoks == db.Coks.FirstOrDefault
                            (q => q.Id == IdCok).Id).IdEom1).Id;
            List <Lection> lections = db.Lections.Where(q=>q.Eom == eom).Include(l=>l.LectionBlocks).ToList();
			return View(db);
        }
        public async Task<IActionResult> Main()
        {
            var Role = HttpContext.User.FindFirst("Role");
            var Group = HttpContext.User.FindFirst("Group");
            var Logins = HttpContext.User.FindFirst("IO");
            var Id = HttpContext.User.FindFirst("Id");
            ViewBag.rar = Logins.Value;
            ViewBag.ror = Group.Value;
            ViewBag.role = Role.Value;
            ViewBag.Id = Id;
            return View(db);
        }
        public async Task<IActionResult> ProfileStudent()
        {
            var Id = HttpContext.User.FindFirst("Id");
            ViewBag.id = Id.Value;
            var Logins = HttpContext.User.FindFirst("IO");
            ViewBag.rar = Logins.Value;
            return View(db);
        }
        [Authorize(Policy = "Teacher")]
        public async Task<IActionResult> ProfileTeacher()
        {
            var Logins = HttpContext.User.FindFirst("IO");
            ViewBag.rar = Logins.Value;
            return View(db);
        }
        [HttpGet]
        public async Task<IActionResult> Trainer(int IdCok, int nowquiz, int score, int timeMinute, int timeSecond)
        {
            ViewBag.nowPage = nowquiz;
            ViewBag.IdCok = IdCok;
            ViewBag.score = score;
            ViewBag.minute = timeMinute;
            ViewBag.second = timeSecond;
            return View(db);
        }
        [HttpGet]
        public async Task<IActionResult> Quiz(int IdCok, int nowquiz, int score, int timeMinute, int timeSecond)
        {
            ViewBag.nowPage = nowquiz;
            ViewBag.IdCok = IdCok;
            ViewBag.score = score;
            ViewBag.minute = timeMinute;
            ViewBag.second = timeSecond;
            return View(db);
        }
		[HttpPost]
		public async Task<IActionResult> QuizReturn(int IdCok, int scoreO, int uscoreO, int timeMinuteO, int timeSecondO, int IdEom, int ballso)
		{
            ResultsTest resultsTest = new ResultsTest();
            resultsTest.QuantityCorrectAnswer = scoreO;
            resultsTest.QuantityUnCorrectAnswer = uscoreO;
            resultsTest.Eom = IdEom;
            resultsTest.Estimation = ballso;
            resultsTest.IdUser = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
			TimeSpan span;
			TimeSpan.TryParse($"{timeMinuteO}:{timeSecondO}", out span);
            resultsTest.TimePeform = span;
            db.ResultsTests.Add(resultsTest);
            db.SaveChanges();
			return RedirectToAction("Quiz", new { IdCok = IdCok, nowquiz = 0, score = 0, timeMinute = 0, timeSecond = 0});
		}
		public async Task<IActionResult> Result(int IdCok, int nowquiz)
        {
            ViewBag.nowPage = nowquiz;
            ViewBag.IdCok = IdCok;
            return View(db);
        }//
		[Authorize(Policy = "Teacher")]
		public async Task<IActionResult> ResultPage()
		{
            string Role = HttpContext.User.FindFirst("Role").ToString();

            ViewBag.ror = Role;
            return View(db);
		}
		
		[HttpPost]
        public async Task<IActionResult> DeleteTestTrainer(int IdVirtual, int IdCok)
        {

            VirtualTrainer virtualTrainer = db.VirtualTrainers.FirstOrDefault(x => x.Id == IdVirtual);
            TestBlock testBlock = db.TestBlocks.FirstOrDefault(x => x.VirtualTrainer == virtualTrainer.Id);
            Test test = db.Tests.FirstOrDefault(x => x.Id == testBlock.Test);
            Quest quest = db.Quests.FirstOrDefault(x => x.Test == test.Id);
            List<Answer> answers = null;
            if (quest != null)
            {

                answers = db.Answers.Where(x => x.Quest == quest.Id).ToList();
            }
            List<TestSequenceBlock> testSequences = db.TestSequenceBlocks.Where(x => x.Test == test.Id).ToList();
            List<TestAccordBlock> testAccord = db.TestAccordBlocks.Where(x => x.Test == test.Id).ToList();
            if (testSequences != null)
            {
                db.TestSequenceBlocks.RemoveRange(testSequences);
                await db.SaveChangesAsync();
            }
            else
            {
                if (testAccord != null)
                {
                    db.TestAccordBlocks.RemoveRange(testAccord);
                    await db.SaveChangesAsync();
                }
                else
                {
                    if (quest != null)
                    {
                        db.TestAccordBlocks.RemoveRange(testAccord);
                        db.Answers.RemoveRange(answers);
                        await db.SaveChangesAsync();
                        db.Quests.Remove(quest);
                        await db.SaveChangesAsync();
                    }
                    else
                    {

                    }
                }
            }


            db.TestBlocks.Remove(testBlock);
            await db.SaveChangesAsync();
            db.Tests.Remove(test);
            await db.SaveChangesAsync();
            db.VirtualTrainers.Remove(virtualTrainer);
            await db.SaveChangesAsync();
            return RedirectToAction("EditTrainerPage", new { IdCok = IdCok });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTest(int IdVirtual, int IdCok)
        {
            
            VirtualTrainer virtualTrainer = db.VirtualTrainers.FirstOrDefault(x => x.Id == IdVirtual);
            TestBlock testBlock = db.TestBlocks.FirstOrDefault(x => x.VirtualTrainer == virtualTrainer.Id);
            Test test = db.Tests.FirstOrDefault(x=> x.Id == testBlock.Test);
            Quest quest = db.Quests.FirstOrDefault(x => x.Test == test.Id);
            List<Answer> answers = null;
            if (quest!=null)
            {

               answers = db.Answers.Where(x => x.Quest == quest.Id).ToList();
            }
            List<TestSequenceBlock> testSequences = db.TestSequenceBlocks.Where(x=>x.Test == test.Id).ToList();
            List<TestAccordBlock> testAccord = db.TestAccordBlocks.Where(x => x.Test == test.Id).ToList();
            if(testSequences != null)
            {
                db.TestSequenceBlocks.RemoveRange(testSequences);
                await db.SaveChangesAsync();
            }
            else
            {
                if (testAccord != null)
                {
                    db.TestAccordBlocks.RemoveRange(testAccord);
                    await db.SaveChangesAsync();
                }
                else
                {
                    if (quest != null)
                    {
                        db.TestAccordBlocks.RemoveRange(testAccord);
                        db.Answers.RemoveRange(answers);
                        await db.SaveChangesAsync();
                        db.Quests.Remove(quest);
                        await db.SaveChangesAsync();
                    }
                    else
                    {

                    }
                }
            }
            
            
            db.TestBlocks.Remove(testBlock);
            await db.SaveChangesAsync();
            db.Tests.Remove(test);
            await db.SaveChangesAsync();
            db.VirtualTrainers.Remove(virtualTrainer);
            await db.SaveChangesAsync();
            return RedirectToAction("EditEomPage", new { IdCok = IdCok});
        }
        [HttpPost]
        public async Task<IActionResult> DeleteLection(int IdLection, int IdCok)
        {
            Lection lection = db.Lections.FirstOrDefault(x=>x.Id == IdLection);
            List<LectionBlock> lectionBlocks = db.LectionBlocks.Where(q => q.IdLection == lection.Id).ToList();
            db.LectionBlocks.RemoveRange(lectionBlocks);
            db.Lections.Remove(lection);
            await db.SaveChangesAsync();
            return RedirectToAction("EditLectionPage", new { IdCok = IdCok });
        }


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
        public async Task<IActionResult> AddCok(Cok a, List<string> EomNames)
        {
            db.Coks.Add(a);

            var eom1 = new Eom { Name = EomNames[0] };
            var eom2 = new Eom { Name = EomNames[1] };
            var eom3 = new Eom { Name = EomNames[2] };
            db.Eoms.Add(eom1);
            db.Eoms.Add(eom2);
            db.Eoms.Add(eom3);
            await db.SaveChangesAsync();

            var content1 = new Content
            {
                IdCoks = a.Id,
                IdEom1 = eom1.Id,
                IdEom2 = eom2.Id,
                IdEom3 = eom3.Id
            };

            db.Contents.Add(content1);
            await db.SaveChangesAsync();

            return RedirectToAction("Main");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCok(int a)
        {
            db.Contents.Remove(db.Contents.FirstOrDefault(q => q.IdCoks == a));
            db.Coks.Remove(db.Coks.FirstOrDefault(q=>q.Id==a));
            
            await db.SaveChangesAsync();
            return RedirectToAction("ProfileTeacher");
        }
        public async Task<IActionResult> AddCok()
        {
            return View(db);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(string role, string group, string Password, string Login, string Surename, string Firstname)
        {
            User user = new User();
            user.Firstname = Firstname;
            user.Password = Password;
            user.Login = Login;
            user.Surename = Surename;
            if(role == "admin")
            {
                user.RoleUser = 3;
            }   
            else if(role == "teacher")
            {
                user.RoleUser = 2;
            }   
            else
            {
                user.RoleUser = 1;
            }

            if (group == "4pk2")
            {
                user.Usergroup = 1;
            }
            else if (group == "3pk2")
            {
                user.Usergroup = 2;
            }
            else
            {
                user.Usergroup = 2;
            }
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Main");
        }
        
        public async Task<IActionResult> AddUser(string test)
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> EditLection(LectionBlock lectionBlock)
        {
            // Обновляем информацию о вопросе
            db.LectionBlocks.Update(lectionBlock);

            // Сохраняем все изменения в базе данных
            await db.SaveChangesAsync();

            // Возвращаем успешный результат в формате JSON
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> AddLection(int IdEom, Lection lection, IFormFile photoa, string icon, List<string> abzac)
        {
            if (photoa != null && photoa.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "image/data");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + photoa.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photoa.CopyToAsync(fileStream);
                }

                // Обновляем поле icon с новым уникальным именем файла
                icon = uniqueFileName;
                Image image = new Image();
                image.Path = uniqueFileName;

                db.Images.Add(image);
                db.SaveChanges();

                lection.Icon = image.Id;
            }

            db.Lections.Add(lection);
            db.SaveChanges();
            if(abzac!=null)
            {
                foreach (string abs in abzac)
                {
                    LectionBlock lectionBlock = new LectionBlock();
                    lectionBlock.Textlection = abs;
                    lectionBlock.IdLection = lection.Id;
                    db.LectionBlocks.Add(lectionBlock);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("Main");
        }
        [HttpPost]
        public async Task<IActionResult> EditTrainer(Quest quest)
        {
            // Обновляем информацию о вопросе
            db.Quests.Update(quest);

            // Сохраняем все изменения в базе данных
            await db.SaveChangesAsync();

            // Возвращаем успешный результат в формате JSON
            return Json(new { success = true });
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
        [HttpPost]
        public async Task<IActionResult> addTest(int IdEom, VirtualTrainer virtualTrainer, Quest quest, List<string> answers)
        {
            db.VirtualTrainers.Add(virtualTrainer);

            Test test = new Test();
            db.Tests.Add(test);
            await db.SaveChangesAsync();

            TestBlock block = new TestBlock();
            block.VirtualTrainer = virtualTrainer.Id;
            block.Test = test.Id;
            block.Textlection = "test";
            db.TestBlocks.Add(block);
            await db.SaveChangesAsync();

            quest.Test = test.Id;
            db.Quests.Add(quest);
            await db.SaveChangesAsync();

            var answer1 = new Answer { TextAnswers = answers[0], IsCorrect = true, Quest = quest.Id };
            var answer2 = new Answer { TextAnswers = answers[1], IsCorrect = false, Quest = quest.Id };
            var answer3 = new Answer { TextAnswers = answers[2], IsCorrect = false, Quest = quest.Id };
            var answer4 = new Answer { TextAnswers = answers[3], IsCorrect = false, Quest = quest.Id };
            db.Answers.AddRange(answer1, answer2, answer3, answer4);
            await db.SaveChangesAsync();

            int idCok = db.Coks.FirstOrDefault(q => q.Id == db.Contents.FirstOrDefault(q => q.IdEom1 == IdEom).IdCoks).Id;
            return RedirectToAction("EditEomPage", new { IdCok = idCok });
        }
        [HttpPost]
        public async Task<IActionResult> addSequensTest(int IdEom, VirtualTrainer virtualTrainer, List<string> sequenceBlocks, List<string> numbers)
        {
            db.VirtualTrainers.Add(virtualTrainer);

            Test test = new Test();
            db.Tests.Add(test);
            await db.SaveChangesAsync();

            TestBlock block = new TestBlock();
            block.VirtualTrainer = virtualTrainer.Id;
            block.Test = test.Id;
            block.Textlection = "test";
            db.TestBlocks.Add(block);
            await db.SaveChangesAsync();

            var testSequensBlock1 = new TestSequenceBlock
            {
                Phrase = sequenceBlocks[0],
                Number = Convert.ToInt32(numbers[0]),
                Test = test.Id
            };
            var testSequensBlock2 = new TestSequenceBlock
            {
                Phrase = sequenceBlocks[1],
                Number = Convert.ToInt32(numbers[1]),
                Test = test.Id
            };
            var testSequensBlock3 = new TestSequenceBlock
            {
                Phrase = sequenceBlocks[2],
                Number = Convert.ToInt32(numbers[2]),
                Test = test.Id
            };
            var testSequensBlock4 = new TestSequenceBlock
            {
                Phrase = sequenceBlocks[3],
                Number = Convert.ToInt32(numbers[3]),
                Test = test.Id
            };
            db.TestSequenceBlocks.AddRange(testSequensBlock1, testSequensBlock2, testSequensBlock3, testSequensBlock4);

            await db.SaveChangesAsync();

            int idCok = db.Coks.FirstOrDefault(q => q.Id == db.Contents.FirstOrDefault(q => q.IdEom3 == IdEom).IdCoks).Id;

            return RedirectToAction("EditEomPage", new { IdCok = idCok });
        }
        
        [HttpPost]
        public async Task<IActionResult> addAccordTest(int IdEom, VirtualTrainer virtualTrainer, List<string> firstPhrases, List<string> secondPhrases)
        {
            db.VirtualTrainers.Add(virtualTrainer);

            Test test = new Test();
            db.Tests.Add(test);
            await db.SaveChangesAsync();

            TestBlock block = new TestBlock();
            block.VirtualTrainer = virtualTrainer.Id;
            block.Test = test.Id;
            block.Textlection = "test";
            db.TestBlocks.Add(block);
            await db.SaveChangesAsync();

            var testAccordBlock1 = new TestAccordBlock
            {
                FirstPhrase = firstPhrases[0],
                SecondPhrase = secondPhrases[0],
                Test = test.Id
            };
            var testAccordBlock2 = new TestAccordBlock
            {
                FirstPhrase = firstPhrases[1],
                SecondPhrase = secondPhrases[1],
                Test = test.Id
            };
            var testAccordBlock3 = new TestAccordBlock
            {
                FirstPhrase = firstPhrases[2],
                SecondPhrase = secondPhrases[2],
                Test = test.Id
            };
            var testAccordBlock4 = new TestAccordBlock
            {
                FirstPhrase = firstPhrases[3],
                SecondPhrase = secondPhrases[3],
                Test = test.Id
            };

            db.TestAccordBlocks.AddRange(testAccordBlock1, testAccordBlock2, testAccordBlock3, testAccordBlock4);

            await db.SaveChangesAsync();

            int idCok = db.Coks.FirstOrDefault(q => q.Id == db.Contents.FirstOrDefault(q => q.IdEom3 == IdEom).IdCoks).Id;

            return RedirectToAction("EditEomPage", new { IdCok = idCok });
        }
        [HttpPost]
        public async Task<IActionResult> addSequensTestTrainer(int IdEom, VirtualTrainer virtualTrainer, List<string> sequenceBlocks, List<string> numbers)
        {
            db.VirtualTrainers.Add(virtualTrainer);

            Test test = new Test();
            db.Tests.Add(test);
            await db.SaveChangesAsync();

            TestBlock block = new TestBlock();
            block.VirtualTrainer = virtualTrainer.Id;
            block.Test = test.Id;
            block.Textlection = "test";
            db.TestBlocks.Add(block);
            await db.SaveChangesAsync();

            var testSequensBlock1 = new TestSequenceBlock
            {
                Phrase = sequenceBlocks[0],
                Number = Convert.ToInt32(numbers[0]),
                Test = test.Id
            };
            var testSequensBlock2 = new TestSequenceBlock
            {
                Phrase = sequenceBlocks[1],
                Number = Convert.ToInt32(numbers[1]),
                Test = test.Id
            };
            var testSequensBlock3 = new TestSequenceBlock
            {
                Phrase = sequenceBlocks[2],
                Number = Convert.ToInt32(numbers[2]),
                Test = test.Id
            };
            var testSequensBlock4 = new TestSequenceBlock
            {
                Phrase = sequenceBlocks[3],
                Number = Convert.ToInt32(numbers[3]),
                Test = test.Id
            };
            db.TestSequenceBlocks.AddRange(testSequensBlock1, testSequensBlock2, testSequensBlock3, testSequensBlock4);

            await db.SaveChangesAsync();

            int idCok = db.Coks.FirstOrDefault(q => q.Id == db.Contents.FirstOrDefault(q => q.IdEom2 == IdEom).IdCoks).Id;

            return RedirectToAction("EditTrainerPage", new { IdCok = idCok });
        }

        [HttpPost]
        public async Task<IActionResult> addAccordTestTrainer(int IdEom, VirtualTrainer virtualTrainer, List<string> firstPhrases, List<string> secondPhrases)
        {
            db.VirtualTrainers.Add(virtualTrainer);

            Test test = new Test();
            db.Tests.Add(test);
            await db.SaveChangesAsync();

            TestBlock block = new TestBlock();
            block.VirtualTrainer = virtualTrainer.Id;
            block.Test = test.Id;
            block.Textlection = "test";
            db.TestBlocks.Add(block);
            await db.SaveChangesAsync();

            var testAccordBlock1 = new TestAccordBlock
            {
                FirstPhrase = firstPhrases[0],
                SecondPhrase = secondPhrases[0],
                Test = test.Id
            };
            var testAccordBlock2 = new TestAccordBlock
            {
                FirstPhrase = firstPhrases[1],
                SecondPhrase = secondPhrases[1],
                Test = test.Id
            };
            var testAccordBlock3 = new TestAccordBlock
            {
                FirstPhrase = firstPhrases[2],
                SecondPhrase = secondPhrases[2],
                Test = test.Id
            };
            var testAccordBlock4 = new TestAccordBlock
            {
                FirstPhrase = firstPhrases[3],
                SecondPhrase = secondPhrases[3],
                Test = test.Id
            };

            db.TestAccordBlocks.AddRange(testAccordBlock1, testAccordBlock2, testAccordBlock3, testAccordBlock4);

            await db.SaveChangesAsync();

            int idCok = db.Coks.FirstOrDefault(q => q.Id == db.Contents.FirstOrDefault(q => q.IdEom2 == IdEom).IdCoks).Id;

            return RedirectToAction("EditTrainerPage", new { IdCok = idCok });
        }

        [HttpPost]
        public async Task<IActionResult> addTestTrainer(int IdEom, VirtualTrainer virtualTrainer, Quest quest, List<string> answers)
        {
            db.VirtualTrainers.Add(virtualTrainer);

            Test test = new Test();
            db.Tests.Add(test);
            await db.SaveChangesAsync();

            TestBlock block = new TestBlock();
            block.VirtualTrainer = virtualTrainer.Id;
            block.Test = test.Id;
            block.Textlection = "test";
            db.TestBlocks.Add(block);
            await db.SaveChangesAsync();

            quest.Test = test.Id;
            db.Quests.Add(quest);
            await db.SaveChangesAsync();

            var answer1 = new Answer { TextAnswers = answers[0], IsCorrect = true, Quest = quest.Id };
            var answer2 = new Answer { TextAnswers = answers[1], IsCorrect = false, Quest = quest.Id };
            var answer3 = new Answer { TextAnswers = answers[2], IsCorrect = false, Quest = quest.Id };
            var answer4 = new Answer { TextAnswers = answers[3], IsCorrect = false, Quest = quest.Id };
            db.Answers.AddRange(answer1, answer2, answer3, answer4);
            await db.SaveChangesAsync();

            int idCok = db.Coks.FirstOrDefault(q => q.Id == db.Contents.FirstOrDefault(q => q.IdEom2 == IdEom).IdCoks).Id;
            return RedirectToAction("EditTrainerPage", new { IdCok = idCok });
        }

        [HttpGet]
        public async Task<IActionResult> EditTrainer(int IdCok)
        {
            ViewBag.f = IdCok;
            if (1 != null)
            {
                Model.OkeiSiteContext cokContext = new OkeiSiteContext();
                if (cokContext != null) return View(cokContext);
            }
            return NotFound();
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
        [HttpGet]
        public async Task<IActionResult> EditLection(int IdCok)
        {
            ViewBag.f = IdCok;
            if (1 != null)
            {
                Model.OkeiSiteContext cokContext = new OkeiSiteContext();
                if (cokContext != null) return View(cokContext);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> addLection(int IdEom, string lection)
        {
            ViewBag.f = IdEom;
            ViewBag.type = lection;
            if (1 != null)
            {
                Model.OkeiSiteContext cokContext = new OkeiSiteContext();
                if (cokContext != null) return View(cokContext);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> addTest(int IdEom, string test)
        {
            ViewBag.f = IdEom;
            ViewBag.type = test;
            if (1 != null)
            {
                Model.OkeiSiteContext cokContext = new OkeiSiteContext();
                if (cokContext != null) return View(cokContext);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> addTestTrainer(int IdEom, string test)
        {
            ViewBag.f = IdEom;
            ViewBag.type = test;
            if (1 != null)
            {
                Model.OkeiSiteContext cokContext = new OkeiSiteContext();
                if (cokContext != null) return View(cokContext);
            }
            return NotFound();
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
        [HttpGet]
        public async Task<IActionResult> EditTrainerPage(int idCok)
        {
            ViewBag.f = idCok;
            if (1 != null)
            {
                Model.OkeiSiteContext cokContext = new OkeiSiteContext();
                if (cokContext != null) return View(cokContext);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> EditEomPage(int idCok)
        {
            ViewBag.f = idCok;
            if (1 != null)
            {
                Model.OkeiSiteContext cokContext = new OkeiSiteContext();
                if (cokContext != null) return View(cokContext);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> EditLectionPage(int idCok)
        {
            ViewBag.f = idCok;
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
        public async Task<IActionResult> EditEomTrainer(Eom eom1)
        {
            db.Eoms.Update(eom1);

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
        public async Task<IActionResult> Edit2Trainer(Answer answer)
        {
            db.Answers.Update(answer);

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
        public async Task<IActionResult> EditAccordTrainer(TestAccordBlock testAccord)
        {
            db.TestAccordBlocks.Update(testAccord);

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
        [HttpPost]
        public async Task<IActionResult> EditSequenceTrainer(TestSequenceBlock testSequence)
        {
            db.TestSequenceBlocks.Update(testSequence);

            await db.SaveChangesAsync();
            return Json(new { success = true });
        }

    }
}
