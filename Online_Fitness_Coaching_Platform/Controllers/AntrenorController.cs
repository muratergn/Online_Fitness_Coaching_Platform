using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Fitness_Coaching_Platform.Data;
using Online_Fitness_Coaching_Platform.Identity;
using Online_Fitness_Coaching_Platform.Models;
using System.Reflection;

namespace Online_Fitness_Coaching_Platform.Controllers
{
    public class AntrenorController : Controller
    {
        public IActionResult Index()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            var kullanici = context.kullanicilar
                      .FirstOrDefault(k => k.Eposta == UserStatic.Email);
            var antrenor = context.antrenorler
                      .FirstOrDefault(k => k.Eposta == UserStatic.Email);
            AntrenorEditModel suankiAntrenor = new AntrenorEditModel
            {
                FirstName=kullanici.Ad,
                LastName =kullanici.Soyad,
                Email = kullanici.Eposta,
                BirthDate = kullanici.DogumTarihi,
                Gender = kullanici.Cinsiyet,
                PhoneNumber = kullanici.TelefonNumarasi,
                Active = kullanici.Etkinlik,
                Status = kullanici.Rolu,
                UzmanlikAlanlari = antrenor.UzmanlikAlanlari,
                Deneyimleri = antrenor.Deneyimleri
            };
            return View(suankiAntrenor);
        }

        [HttpPost]
        public async Task<IActionResult> BilgiGuncelleme(AntrenorAddModel model)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);
            var kullaniciBul = context.kullanicilar.FirstOrDefault(k => k.Eposta == model.Email);
            var antrenorBul = context.antrenorler.FirstOrDefault(k => k.Eposta == model.Email);

            if (kullaniciBul != null)
                {
                    if (model.Active == "Aktif")
                        kullaniciBul.Etkinlik = "Aktif";
                    else
                        kullaniciBul.Etkinlik = "Pasif";
                    kullaniciBul.Ad = model.FirstName;
                    antrenorBul.Ad = model.FirstName;
                    kullaniciBul.Soyad= model.LastName;
                    antrenorBul.Soyad = model.LastName;
                    kullaniciBul.DogumTarihi = model.BirthDate;
                    kullaniciBul.TelefonNumarasi= model.PhoneNumber;
                    antrenorBul.UzmanlikAlanlari = model.UzmanlikAlanlari;
                    antrenorBul.Deneyimleri = model.Deneyimleri;
                    context.SaveChanges();
                }
            
            return RedirectToAction("Index");
        }

        public IActionResult DanisanListele()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            int AntrenorId = context.antrenorler
                .Where(antrenor => antrenor.Eposta == UserStatic.Email)
                .Select(antrenor => antrenor.Id)
                .FirstOrDefault();

            List<Kullanici> danisanKullanicilar = context.kullaniciAntrenor
                .Where(ka => ka.AntrenorId == AntrenorId)
                .Join(context.kullanicilar,
                    ka => ka.DanisanId,
                    kullanici => kullanici.Id,
                        (ka, kullanici) => new Kullanici
                        {
                            Id = kullanici.Id,
                            Ad = kullanici.Ad,
                            Soyad = kullanici.Soyad,
                            DogumTarihi = kullanici.DogumTarihi,
                            Cinsiyet = kullanici.Cinsiyet,
                            Eposta = kullanici.Eposta,
                            TelefonNumarasi = kullanici.TelefonNumarasi,
                            Etkinlik = kullanici.Etkinlik,
                            Rolu = kullanici.Rolu
                        })
                        .ToList();
            return View(danisanKullanicilar);
        }

        public IActionResult AntrenmanProgramıHazırla()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AntrenmanProgramıHazırla(AntrenmanProgrami model)
        {
            
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            int AntrenorId = context.antrenorler
                .Where(antrenor => antrenor.Eposta == UserStatic.Email)
                .Select(antrenor => antrenor.Id)
                .FirstOrDefault();

            model.YapanId = AntrenorId;
            context.antrenmanProgramlari.Add(model);
            context.SaveChanges();
            bool modelVarMi = context.antrenmanProgramlari.Any(ap => ap.Id == model.Id);
            if(modelVarMi) 
            {
                TempData["SuccessMessage2"] = "Antrenman programı başarıyla kaydedildi.";
                return View();
            }
            return View(model);
        }
        public IActionResult BeslenmeProgramıHazırla()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BeslenmeProgramıHazırla(BeslenmeProgrami model)
        {

            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            int AntrenorId = context.antrenorler
                .Where(antrenor => antrenor.Eposta == UserStatic.Email)
                .Select(antrenor => antrenor.Id)
                .FirstOrDefault();

            model.YapanId = AntrenorId;
            context.beslenmeProgramlari.Add(model);
            context.SaveChanges();
            bool modelVarMi = context.beslenmeProgramlari.Any(ap => ap.Id == model.Id);
            if (modelVarMi)
            {
                TempData["SuccessMessage3"] = "Beslenme programı başarıyla kaydedildi.";
                return View();
            }
            return View(model);
        }

        public IActionResult MesajAt()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MesajAt(Mesajlasma model)
        {

            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            int AntrenorId = context.antrenorler
                .Where(antrenor => antrenor.Eposta == UserStatic.Email)
                .Select(antrenor => antrenor.Id)
                .FirstOrDefault();

            model.GonderenId = AntrenorId;
            context.mesajlasmalar.Add(model);
            context.SaveChanges();
            bool modelVarMi = context.mesajlasmalar.Any(ap => ap.Id == model.Id);
            if (modelVarMi)
            {
                TempData["SuccessMessage5"] = "Mesajınız başarıyla gönderilmiştir.";
                return View();
            }
            return View(model);
        }

        public IActionResult MesajGoruntule()
        {

            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            int AntrenorId = context.antrenorler
                .Where(antrenor => antrenor.Eposta == UserStatic.Email)
                .Select(antrenor => antrenor.Id)
                .FirstOrDefault();

            var mesajlasma = context.mesajlasmalar
                                  .Where(ap => ap.AliciId == AntrenorId)
                                  .ToList();

            return View(mesajlasma);
        }
    }
}
