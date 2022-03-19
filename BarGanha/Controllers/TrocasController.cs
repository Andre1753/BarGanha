using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarGanha.Controllers
{
    public class TrocasController : Controller
    {
        // GET: TrocasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TrocasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrocasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrocasController/Create
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

        // GET: TrocasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrocasController/Edit/5
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

        // GET: TrocasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrocasController/Delete/5
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
