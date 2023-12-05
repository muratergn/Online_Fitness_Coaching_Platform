using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Fitness_Coaching_Platform.Identity;
using Online_Fitness_Coaching_Platform.Models;

namespace Online_Fitness_Coaching_Platform.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            var kullanici = context.kullanicilar
                      .FirstOrDefault(k => k.Eposta == UserStatic.Email);
            KullaniciEditModel suankiKullanici = new KullaniciEditModel
            {
                Ad = kullanici.Ad,
                Soyad = kullanici.Soyad,
                Eposta = kullanici.Eposta,
                DogumTarihi = kullanici.DogumTarihi,
                Cinsiyet = kullanici.Cinsiyet,
                TelefonNumarasi = kullanici.TelefonNumarasi,
                Aktiflik = kullanici.Etkinlik, 
            };
            return View(suankiKullanici);
        }

        [HttpPost]
        public async Task<IActionResult> BilgiGuncelleme(KullaniciEditModel model)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);
            var kullaniciBul = context.kullanicilar.FirstOrDefault(k => k.Eposta == model.Eposta);

            if (kullaniciBul != null)
            {
                if (model.Aktiflik == "Aktif")
                    kullaniciBul.Etkinlik = "Aktif";
                else
                    kullaniciBul.Etkinlik = "Pasif";
                kullaniciBul.Ad = model.Ad;
                kullaniciBul.Soyad = model.Soyad;
                kullaniciBul.DogumTarihi = model.DogumTarihi;
                kullaniciBul.TelefonNumarasi = model.TelefonNumarasi;
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult AntrenmanProgramlari() 
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            int KullaniciId = context.kullanicilar
                .Where(kullanici => kullanici.Eposta == UserStatic.Email)
                .Select(kullanici => kullanici.Id)
                .FirstOrDefault();

            var antrenmanProgramlaris =context.antrenmanProgramlari
                                  .Where(ap => ap.AliciId == KullaniciId)
                                  .ToList();

            return View(antrenmanProgramlaris);
        }

        public IActionResult BeslenmeProgramlari()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer("Server=database-1.cwmsh31myn6x.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=DByazlab;User Id=admin;Password=zakuska123;TrustServerCertificate=True");
            var context = new RepositoryContext(optionsBuilder.Options);

            int KullaniciId = context.kullanicilar
                .Where(kullanici => kullanici.Eposta == UserStatic.Email)
                .Select(kullanici => kullanici.Id)
                .FirstOrDefault();

            var beslenmeProgramis = context.beslenmeProgramlari
                                  .Where(ap => ap.AliciId == KullaniciId)
                                  .ToList();

            return View(beslenmeProgramis);
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

            int KullaniciId = context.kullanicilar
                .Where(kullanici => kullanici.Eposta == UserStatic.Email)
                .Select(kullanici => kullanici.Id)
                .FirstOrDefault();

            model.GonderenId = KullaniciId;
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

            int KullaniciId = context.kullanicilar
                .Where(kullanici => kullanici.Eposta == UserStatic.Email)
                .Select(kullanici => kullanici.Id)
                .FirstOrDefault();

            var mesajlasma = context.mesajlasmalar
                                  .Where(ap => ap.AliciId == KullaniciId)
                                  .ToList();

            return View(mesajlasma);
        }


    }
}
