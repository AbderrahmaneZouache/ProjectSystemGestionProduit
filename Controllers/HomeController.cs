using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ProjetCatalogueProduit.Models;

namespace ProjetCatalogueProduit.Controllers
{
    public class HomeController : Controller
    {
        BD_CATALOGUE_PRODUITEntities db = new BD_CATALOGUE_PRODUITEntities();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList().OrderBy(r => r.LIBELLE_CATEGORIE);
            return View();
        }
    }
}