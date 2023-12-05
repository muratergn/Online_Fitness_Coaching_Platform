using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Online_Fitness_Coaching_Platform.Identity;
using Online_Fitness_Coaching_Platform.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Online_Fitness_Coaching_Platform.Controllers
{
    public class AdminController : Controller
    {

        private UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Index()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            List<KullaniciAktiflikModel> tumKullanicilar = context.kullanicilar
                .Select(k => new KullaniciAktiflikModel{
                    Isim = k.Ad,
                    Soyisim = k.Soyad,
                    Eposta= k.Eposta,
                    Rol = k.Rolu,
                    Aktiflik = k.Etkinlik,
                    AktiflikDurum=k.Etkinlik
                })
                .ToList();

            List<SelectListItem> degerler = new List<SelectListItem>();
            degerler.Add(new SelectListItem { Text = "Pasif", Value = "0" });
            degerler.Add(new SelectListItem { Text = "Aktif", Value = "1" });
            ViewBag.degerler = degerler;

            return View(tumKullanicilar);
        }

        [HttpPost]
        public async Task<IActionResult> Aktiflik(List<KullaniciAktiflikModel> model)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            foreach (var kullanici in model) 
            {

                var kullaniciBul = context.kullanicilar.FirstOrDefault(k => k.Eposta == kullanici.Eposta);

                if (kullaniciBul != null) 
                {
                    if(kullanici.AktiflikDurum == "1")
                        kullaniciBul.Etkinlik = "Aktif";
                    else
                        kullaniciBul.Etkinlik = "Pasif";
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult AntrenorEkle()
        {
            return View();
        }

        public IActionResult KullaniciEkle() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KullaniciEkle(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Random random = new Random();
            int rastgeleSayi = random.Next(0, 10000);

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = rastgeleSayi.ToString(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
                optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");

                using (var context = new RepositoryContext(optionsBuilder.Options))
                {
                    var yeniKullanici = new Kullanici
                    {
                        Id = rastgeleSayi,
                        Ad = model.FirstName,
                        Soyad = model.LastName,
                        DogumTarihi = model.BirthDate,
                        Cinsiyet = model.Gender,
                        Eposta = model.Email,
                        TelefonNumarasi = model.PhoneNumber,
                        Sifre = Hash(model.Password),
                        Etkinlik = "Pasif",
                        Rolu = model.Status,
                        ProfilFotosu = " ",
                    };
                    if (model.Status.Equals("Antrenör"))
                    {
                        var yeniAntrenör = new Antrenor
                        {
                            Id = rastgeleSayi,
                            Ad = model.FirstName,
                            Soyad = model.LastName,
                            UzmanlikAlanlari = " ",
                            Deneyimleri = " ",
                            Eposta = model.Email
                        };
                        context.kullanicilar.Add(yeniKullanici);
                        context.antrenorler.Add(yeniAntrenör);
                    }
                    else
                        context.kullanicilar.Add(yeniKullanici);
                    await context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Kullanıcı başarıyla kaydedildi.";
                }

                return View();
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
        public string Hash(string plainPassword)
        {
            var passwordHasher = new PasswordHasher<object>();

            string hashedPassword = passwordHasher.HashPassword(null, plainPassword);

            return hashedPassword;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AntrenorEkle(AntrenorAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Random random = new Random();
            int rastgeleSayi = random.Next(0, 10000);

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = rastgeleSayi.ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
                optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");

                using (var context = new RepositoryContext(optionsBuilder.Options))
                {
                    var yeniKullanici = new Kullanici
                    {
                        Id = rastgeleSayi,
                        Ad = model.FirstName,
                        Soyad = model.LastName,
                        DogumTarihi = model.BirthDate,
                        Cinsiyet = model.Gender,
                        Eposta = model.Email,
                        TelefonNumarasi = model.PhoneNumber,
                        Sifre = Hash(model.Password),
                        Etkinlik = "Pasif",
                        Rolu = "Antrenör",
                        ProfilFotosu = " ",
                    };
                    
                    var yeniAntrenör = new Antrenor
                    {
                        Id = rastgeleSayi,
                        Ad = model.FirstName,
                        Soyad = model.LastName,
                        UzmanlikAlanlari = model.UzmanlikAlanlari,
                        Deneyimleri = model.Deneyimleri,
                        Eposta = model.Email
                    };
                        context.kullanicilar.Add(yeniKullanici);
                        context.antrenorler.Add(yeniAntrenör);
                    
                    await context.SaveChangesAsync();
                    TempData["SuccessMessage1"] = "Antrenör başarıyla kaydedildi.";
                }

                return View();
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
        public IActionResult KullaniciListele() 
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            List<RegisterModel> tumKullanicilar = context.kullanicilar
                .Select(k => new RegisterModel
                {
                    FirstName = k.Ad,
                    LastName = k.Soyad,
                    Email = k.Eposta,
                    Status = k.Rolu,
                    Active = k.Etkinlik,
                    Gender = k.Cinsiyet,
                    BirthDate = k.DogumTarihi,

                })
                .ToList();

            return View(tumKullanicilar);
        }

        public IActionResult AntrenorListele()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            List<AntrenorAddModel> tumKullanicilar = context.antrenorler
                .Select(k => new AntrenorAddModel
                {
                    FirstName = k.Ad,
                    LastName = k.Soyad,
                    Email = k.Eposta,
                    UzmanlikAlanlari=k.UzmanlikAlanlari,
                    Deneyimleri = k.Deneyimleri
                })
                .ToList();

            return View(tumKullanicilar);
        }

        
        public IActionResult AntrenorAtama()
        {
            KullaniciAntrenor kullaniciAntrenor = new KullaniciAntrenor();
            return View(kullaniciAntrenor);
        }

        [HttpPost]
        public IActionResult AntrenorAtama(KullaniciAntrenor kullaniciAntrenor)
        {
            Random rastgele = new Random();
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            List<Antrenor> istenenAntrenorler = context.antrenorler
                     .Where(antrenor => !context.kullaniciAntrenor.Any(ka => ka.AntrenorId == antrenor.Id) ||
                     context.kullaniciAntrenor.Count(ka => ka.AntrenorId == antrenor.Id) < 6)
            .Select(antrenor => new Antrenor
            {
                Id = antrenor.Id,
                Eposta = antrenor.Eposta,
            })
            .ToList();

            List<Kullanici> ogrenciKullanicilar = context.kullanicilar
                        .Where(kullanici => kullanici.Rolu == "Öğrenci" &&
                        !context.kullaniciAntrenor.Any(ka => ka.DanisanId == kullanici.Id))
            .ToList();

            while(istenenAntrenorler.Count!=0 && ogrenciKullanicilar.Count!=0)
            {
                int kullaniciId = rastgele.Next(ogrenciKullanicilar.Count);
                int antrenorId = rastgele.Next(istenenAntrenorler.Count); ;

                KullaniciAntrenor yeniKullaniciAntrenor = new KullaniciAntrenor
                {
                    DanisanId = kullaniciId,
                    AntrenorId = antrenorId,
                    
                };

                context.kullaniciAntrenor.Add(yeniKullaniciAntrenor);
                context.SaveChanges();

                istenenAntrenorler = context.antrenorler
                        .Where(antrenor => !context.kullaniciAntrenor.Any(ka => ka.AntrenorId == antrenor.Id) ||
                        context.kullaniciAntrenor.Count(ka => ka.AntrenorId == antrenor.Id) < 6)
                     .Select(antrenor => new Antrenor
                     {
                        Id = antrenor.Id,
                        Eposta = antrenor.Eposta,
                     })
                .ToList();

                ogrenciKullanicilar = context.kullanicilar
                            .Where(kullanici => kullanici.Rolu == "Öğrenci" &&
                            !context.kullaniciAntrenor.Any(ka => ka.DanisanId == kullanici.Id))
                .ToList();
            }



            return View();
        }
    }
}
