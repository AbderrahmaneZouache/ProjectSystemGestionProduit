using ProjetCatalogueProduit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

namespace ProjetCatalogueProduit.Controllers
{
    
    public class ProduitController : Controller
    {
        BD_CATALOGUE_PRODUITEntities db = new BD_CATALOGUE_PRODUITEntities();
        // GET: Produit
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AjoutProduit()
        {

            try
            {
                ViewBag.listeProduit = db.CAT_PRODUIT.ToList();
                ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList();
                return View();
            }
            catch (Exception)
            {

                return HttpNotFound();
            }


        }
        [HttpPost]
        public ActionResult AjoutProduit(CAT_PRODUIT produit)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];//le nom de notre fichier
                        if (file != null && Request.Files.Count > 0)//si notre fichier est different de null et que sa taille > 0
                        {
                            var fileName = Path.GetFileName(file.FileName);//recuperer le nom du fichier
                            var path = Path.Combine(Server.MapPath("~/Fichier"), fileName);// recuperer le chemain d'acces ou sera mis notre fichier
                            file.SaveAs(path);//enregistrer le tout dans le serveur

                            produit.IMAGE_PRODUIT = fileName;
                            produit.URL_IMAGE_PRODUIT = "/Fichier";
                        }
                    }
                        produit.DATE_SAISIE = DateTime.Now;
                        db.CAT_PRODUIT.Add(produit);
                        db.SaveChanges();

                    }
                    return RedirectToAction("AjoutProduit");
                }
            catch (Exception)
            {

                return HttpNotFound();
            }

        
        }
        public ActionResult SupprimerProduit(int id)
        {
            try
            {
                CAT_PRODUIT produit = db.CAT_PRODUIT.Find(id);
                if (produit != null)
                {
                    db.CAT_PRODUIT.Remove(produit);
                    db.SaveChanges();

                }
                return RedirectToAction("AjoutProduit");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult ModifierProduit(int id)
        {
            try
            {
                ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList();
                ViewBag.listeProduit = db.CAT_PRODUIT.ToList();

                CAT_PRODUIT produit = db.CAT_PRODUIT.Find(id);
                if (produit != null)
                {
                    return View("AjoutProduit", produit);
                }
                return RedirectToAction("AjoutProduit");
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        [HttpPost]
        public ActionResult ModifierProduit(CAT_PRODUIT produit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produit.DATE_SAISIE = DateTime.Now;
                    db.Entry(produit).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("AjoutProduit");
            }
            catch (Exception)
            {

                return HttpNotFound();
                ;
            }
        }
    }
}