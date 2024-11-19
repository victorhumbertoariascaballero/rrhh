using MIDIS.ORI.Entidades;
using MIDIS.ORI.Entidades.Base;
using MIDIS.ORI.LogicaNegocio;
using MIDIS.Utiles;
using MIDIS.UtilesMVC;
using MIDIS.UtilesMVC.Filtros;
using MIDIS.UtilesWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBoletas.Controllers
{
    public class LogErrorController : Controller
    {
        // GET: LogError
        public ActionResult Index()
        {
            return View();
        }

        // GET: LogError/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LogError/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogError/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LogError/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LogError/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LogError/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LogError/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
