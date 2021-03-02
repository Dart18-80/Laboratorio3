using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Laboratorio3.Models;
using LibreriadeClasesED;
using Laboratorio3.Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace Laboratorio3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(ILogger<HomeController> logger,IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateCSV()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CreateCSV(InventarioMedicina model)
        {
            string uniqueFileName = null;
            if (model.FileC!=null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Upload");
                uniqueFileName = model.FileC.FileName;
                string filepath = Path.Combine(uploadsFolder, uniqueFileName);
                if (!System.IO.File.Exists(filepath))
                {
                    using (var INeadLearn= new FileStream(filepath, FileMode.CreateNew))
                    {
                        model.FileC.CopyTo(INeadLearn);
                    }
                }

                string ccc = System.IO.File.ReadAllText(filepath);
                foreach (string row in ccc.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        if (row.Split(',')[0] != "id")
                        {
                            var result=Regex.Split(row, "(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)");
                            Singleton.Instance.ListaJugador.AddHead(new InventarioMedicina
                            {
                                Id=Convert.ToString(result[1]),
                                NombreMedicina=Convert.ToString(result[3]),
                                Descripcion = Convert.ToString(result[5]),
                                CasaProductora = Convert.ToString(result[7]),
                                Precio = Convert.ToString(result[9]),
                                Existencia=Convert.ToInt32(result[11])

                            });
                        }
                    }
                }
                return RedirectToAction("ListaMedicina");
            }
            return View();
        }
        public IActionResult ListaMedicina()
        {
            Singleton.Instance.Nueva.Clear();
            Singleton.Instance.Procedimiento.Mostrar(Singleton.Instance.ListaJugador.Header, Singleton.Instance.Nueva);

            return View(Singleton.Instance.Nueva);
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
