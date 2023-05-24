using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockMvcProject.Models.Entity;
using PagedList;
//using PagedList.Mvc;
namespace StockMvcProject.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var urunler = db.TBLURUNLER.ToList();
            return View(urunler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            //Listeden öğe seç
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text=i.KATEGORIAD,
                                                 Value=i.KATEGORIID.ToString()
                                             }
                                             ).ToList();
            //Controllerda bulunan değerleri sayfaya taşır.
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER urun)
        {
            var ktgr = db.TBLKATEGORILER.Where(item=>item.KATEGORIID==urun.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.TBLKATEGORILER = ktgr;
            db.TBLURUNLER.Add(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }
                                           ).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir",urun);
        }

        public ActionResult Guncelle(TBLURUNLER u)
        {
            var urun = db.TBLURUNLER.Find(u.URUNID);
            urun.URUNAD = u.URUNAD;
            urun.MARKA = u.MARKA;
            urun.STOK = u.STOK;
            //urun.URUNKATEGORI = u.URUNKATEGORI;
            var ktgr = db.TBLKATEGORILER.Where(m => m.KATEGORIID == u.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktgr.KATEGORIID;
            urun.FIYAT = u.FIYAT;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }

}