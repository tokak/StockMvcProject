using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockMvcProject.Models.Entity;
namespace StockMvcProject.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBLMUSTERILER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());
            //var musteriler = db.TBLMUSTERILER.ToList();
            //for (int i = 0; i < musteriler.Count; i++)
            //{
            //    ViewBag.Id = musteriler[i].MUSTERIID;
            //    ViewBag.Ad = musteriler[i].MUSTERIAD;
            //    ViewBag.Soyad = musteriler[i].MUSTERISOYAD;
            //}
            //return View(musteriler);
        }

        [HttpGet]
        public ActionResult MusteriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MusteriEkle(TBLMUSTERILER musteri)
        {
            if (!ModelState.IsValid)
            {
                return View("MusteriEkle");
            }
            db.TBLMUSTERILER.Add(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir",musteri);
        }
        public ActionResult Guncelle(TBLMUSTERILER m)
        {
            var musteri = db.TBLMUSTERILER.Find(m.MUSTERIID);
            musteri.MUSTERIAD = m.MUSTERIAD;
            musteri.MUSTERISOYAD = m.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}