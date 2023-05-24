using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockMvcProject.Models.Entity;
using PagedList;
using PagedList.Mvc;



namespace StockMvcProject.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLKATEGORILER.ToList();
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa,4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORILER kategori)
        {
            //doğrulama işlemi yapılmadıysa
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      
        public ActionResult Sil(int id)
        {
            var kategori = db.TBLKATEGORILER.Where(item => item.KATEGORIID == id).FirstOrDefault();
            db.TBLKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");    
        }
        [HttpGet]
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir",ktgr);
        }
       
        public ActionResult Guncelle(TBLKATEGORILER ktgr)
        {
            var kategori = db.TBLKATEGORILER.Find(ktgr.KATEGORIID);
            kategori.KATEGORIAD = ktgr.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}