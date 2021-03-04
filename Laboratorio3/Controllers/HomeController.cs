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
        string idparacliente=null;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment)
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
        delegate int Delagados(MedicinasBinario Med, string Med1);//llamar delegado
        delegate int Delagado(InventarioMedicina Med);//llamar delegado

        InventarioMedicina LlamadoClass = new InventarioMedicina();
        [HttpPost]
        public IActionResult CreateCSV(InventarioMedicina model)
        {
            string uniqueFileName = null;
            if (model.FileC != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Upload");
                uniqueFileName = model.FileC.FileName;
                string filepath = Path.Combine(uploadsFolder, uniqueFileName);
                if (!System.IO.File.Exists(filepath))
                {
                    using (var INeadLearn = new FileStream(filepath, FileMode.CreateNew))
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
                            var result = Regex.Split(row, "(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)");
                            Singleton.Instance.ListaJugador.AddHead(new InventarioMedicina
                            {
                                NombreMedicina = Convert.ToString(result[3]).Replace('"', ' '),
                                Id = Convert.ToString(result[1]),
                                Descripcion = Convert.ToString(result[5]).Replace('"', ' '),
                                CasaProductora = Convert.ToString(result[7]).Replace('"', ' '),
                                Precio = Convert.ToString(result[9]),
                                Existencia = Convert.ToInt32(result[11])
                            });
                            if (Convert.ToInt32(result[11])!=0)
                            {
                                Delagados InvocarNombre = new Delagados(LlamadoClass.CompareNameMedi);
                                //Singleton.Instance.AccesoArbol.Add(Singleton.Instance.ListaJugador.Header, InvocarNombre);
                            }
                        }
                    }
                }
                return RedirectToAction("IngresoPedido");
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
        public IActionResult Abastecer()
        {
            
            return View();
        }
        public IActionResult IngresoPedido()
        {
            return View();
        }
        [HttpPost]
        public IActionResult IngresoPedido(IFormCollection collection, string IddeNit)
        {
            ViewData["CurrentNit"] = IddeNit;
            idparacliente = IddeNit;
            string dine = "$546.2";
            string a=dine.Replace("$", string.Empty);
            double precio = Convert.ToDouble(a.ToString()) ;
            try
            {
                var NuevoCliente = new Models.Cliente  
                {
                    NombreCliente = collection["NombreCliente"],
                    DireccionCliente=collection["DireccionCliente"],
                    Nit = IddeNit,
                    Idcliente = Convert.ToInt32(Cliente.cont++)
                };
                Singleton.Instance.ListaCliente.Add(NuevoCliente);
                return RedirectToAction("AgregarBuscarMedicina");
            }
            catch
            {
                return View();
            }
        }
        public IActionResult AgregarBuscarMedicina(string SSearch)
        {
            ViewData["CurrentFilterSearch"] = SSearch;

            return View(Singleton.Instance.ListExistencia);
        }
        public IActionResult Agregar() 
        {
            return View(); 
        }
    public IActionResult OrdenCliente() 
        {
            string Nombre = Singleton.Instance.ListaCliente[Cliente.cont - 1].NombreCliente;
            string Direccion = Singleton.Instance.ListaCliente[Cliente.cont - 1].DireccionCliente;
            string Nit = Singleton.Instance.ListaCliente[Cliente.cont - 1].Nit;


            ViewData["Nombre"] = Nombre;
            ViewData["Direccion"] = Direccion;
            ViewData["Nit"] = Nit;
            ViewData["Medi"] = "";
            ViewData["Total"] = "";
            return View() ;
        }
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
