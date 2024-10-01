using Business_Entity;
using CRUD_NOTAS.Models;
using Data_Access;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Diagnostics;

namespace CRUD_NOTAS.Controllers
{
    public class HomeController : Controller
    {
        private readonly DANotas _DANotas;

        public HomeController(DANotas dANotas)
        {
            _DANotas = dANotas;
        }

        public IActionResult Index()
        {
            List<BENotas> lstNotas= _DANotas.ListNotas();
            ViewBag.Data = lstNotas;
            return View();
        }

        public IActionResult GetData()
        {
            List<BENotas> lstNotas = _DANotas.ListNotas();
            ViewBag.Data = lstNotas;
            return PartialView("_Table");
        }

        [HttpPost]
        public IActionResult Create([FromBody] BENotas _BENotas)
        {
            if (ModelState.IsValid)
            {
                _DANotas.InsertNotas(_BENotas);

                return Json(new { success = true, message = "Registro creado con éxito." });
            }

            return Json(new { success = false, message = "Error al validar los datos." });
        }

        [HttpPut]
        public IActionResult Edit([FromBody] BENotas _BENotas)
        {
            if (ModelState.IsValid)
            {
                _DANotas.EditNotas(_BENotas);

                return Json(new { success = true, message = "Registro editado con éxito." });
            }

            return Json(new { success = false, message = "Error al validar los datos." });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _DANotas.DeleteNotas(id);

                return Json(new { success = true, message = "Registro eliminado con éxito." });
            }

            return Json(new { success = false, message = "No se pudo eliminar el registro." });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}