using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarGanha.Controllers
{
    public class SolicitacoesTrocasController : Controller
    {
        // GET: SolicitacoesTrocasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SolicitacoesTrocasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SolicitacoesTrocasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SolicitacoesTrocasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SolicitacoesTrocasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SolicitacoesTrocasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SolicitacoesTrocasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SolicitacoesTrocasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
