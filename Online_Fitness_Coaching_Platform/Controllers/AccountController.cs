using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Online_Fitness_Coaching_Platform.Identity;
using Online_Fitness_Coaching_Platform.Models;
using Entities;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Online_Fitness_Coaching_Platform.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model) 
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            Random random = new Random();
            int rastgeleSayi = random.Next(0, 10000);

            var user = new User()
            {
                FirstName= model.FirstName,
                LastName= model.LastName,
                Email= model.Email,
                PhoneNumber= model.PhoneNumber,
                UserName= rastgeleSayi.ToString()
            };

            var result = await _userManager.CreateAsync(user,model.Password);

            if (result.Succeeded) 
            {
                var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
                optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");

                using (var context = new RepositoryContext(optionsBuilder.Options))
                {
                    var yeniKullanici = new Kullanici
                    {
                        Id= rastgeleSayi,
                        Ad = model.FirstName,
                        Soyad = model.LastName,
                        DogumTarihi = model.BirthDate,
                        Cinsiyet = model.Gender,
                        Eposta = model.Email,
                        TelefonNumarasi = model.PhoneNumber,
                        Sifre = Hash(model.Password),
                        Etkinlik = "Pasif",
                        Rolu = model.Status,
                        ProfilFotosu= " ",
                    };
                    if (model.Status.Equals("Antrenör"))
                    {
                        var yeniAntrenör = new Antrenor
                        {
                            Id= rastgeleSayi,
                            Ad = model.FirstName,
                            Soyad = model.LastName,
                            UzmanlikAlanlari= " ",
                            Deneyimleri= " ",
                            Eposta= model.Email
                        };
                        context.kullanicilar.Add(yeniKullanici);
                        context.antrenorler.Add(yeniAntrenör);
                    }
                    else
                        context.kullanicilar.Add(yeniKullanici);
                    await context.SaveChangesAsync();
                    
                }

                return RedirectToAction("Login", "Account");
            }
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }



            return View(model);
      
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu Email ile daha önce hesap oluşturulmamış");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
                var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
                optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
                using (var context = new RepositoryContext(optionsBuilder.Options)) 
                {
                    var kullaniciRol = context.kullanicilar
                              .Where(k => k.Eposta == model.Email)
                              .Select(k => k.Rolu)
                              .FirstOrDefault();
                    var kullaniciIsim = context.kullanicilar
                              .Where(k => k.Eposta == model.Email)
                              .Select(k => k.Ad + " " + k.Soyad)
                              .FirstOrDefault();

                    if (kullaniciRol == "Öğrenci")
                    {
                        TempData["kullancigiris"] = kullaniciIsim;
                        UserStatic.Email = model.Email;
                        return RedirectToAction("Index", "Student");
                    }
                    else if(kullaniciRol == "Antrenör")
                    {
                        TempData["kullancigiris"] = kullaniciIsim;
                        UserStatic.Email = model.Email;
                        return RedirectToAction("Index", "Antrenor");
                    }
                    else if (kullaniciRol == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                }
            }

            ModelState.AddModelError("", "Girilen email veya parola yanlış");
            return View(model);
        }

        public string Hash(string plainPassword)
        {
            var passwordHasher = new PasswordHasher<object>();
            
            string hashedPassword = passwordHasher.HashPassword(null, plainPassword);

            return hashedPassword;
        }
    }
}
