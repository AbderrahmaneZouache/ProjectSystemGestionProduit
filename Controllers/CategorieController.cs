using ProjetCatalogueProduit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ProjetCatalogueProduit.Controllers
{
    public class CategorieController : Controller
    {
        BD_CATALOGUE_PRODUITEntities db = new BD_CATALOGUE_PRODUITEntities();
        // GET: Categorie
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AjoutCategorie()
        {

            try
            {
                ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList();
                return View();
            }
            catch (Exception )
            {

                return HttpNotFound();
            }

           
        }
        [HttpPost]
        public ActionResult AjoutCategorie(CAT_CATEGORIE categorie)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    categorie.DATE_SAISIE = DateTime.Now;
                    db.CAT_CATEGORIE.Add(categorie);
                    db.SaveChanges();
                }
                return RedirectToAction("AjoutCategorie");
            }
            catch (Exception)
            {

                return HttpNotFound();
            }


        }

        public ActionResult SupprimerCategorie(int id)
        {
            try
            {
                CAT_CATEGORIE categorie = db.CAT_CATEGORIE.Find(id);
                if (categorie != null)
                {
                    db.CAT_CATEGORIE.Remove(categorie);
                    db.SaveChanges();

                }
                return RedirectToAction("AjoutCategorie");
            }
            catch (Exception )
            {

                throw;
            }
        }

        public ActionResult ModifierCategorie(int id )
        {
            try
            {
                ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList();
                CAT_CATEGORIE categorie = db.CAT_CATEGORIE.Find(id);
                if (categorie!=null)
                {
                    return View("AjoutCategorie", categorie);
                }
                return RedirectToAction("AjoutCategorie");
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        [HttpPost]
        public ActionResult ModifierCategorie(CAT_CATEGORIE categorie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categorie.DATE_SAISIE = DateTime.Now;
                    db.Entry(categorie).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("AjoutCategorie");
            }
            catch (Exception)
            {

                return HttpNotFound();
 ;            }
        }

    }
}