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
        delegate int Delagados(MedicinasBinario Med1, MedicinasBinario Med2); //llamar delegado
        delegate int DelegadoString(MedicinasBinario Med1, string nombre);
        delegate int Delagado(InventarioMedicina Med);//llamar delegado

        MedicinasBinario LlamadoMedBinario = new MedicinasBinario();
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
                            InventarioMedicina Nuevo = new InventarioMedicina { 
                                NombreMedicina = Convert.ToString(result[3]).Replace('"', ' '),
                                Id = Convert.ToString(result[1]),
                                Descripcion = Convert.ToString(result[5]).Replace('"', ' '),
                                CasaProductora = Convert.ToString(result[7]).Replace('"', ' '),
                                Precio = Convert.ToString(result[9]),
                                Existencia = Convert.ToInt32(result[11])
                            };
                            Nodo<InventarioMedicina> Direccion = Singleton.Instance.ListaJugador.AddHead(Nuevo);
                            MedicinasBinario IngresoArbol = new MedicinasBinario
                            {
                                Nombre = Convert.ToString(result[3]).Replace('"', ' '),
                                Posicion = Direccion
                            };
                            if (Convert.ToInt32(result[11])!=0)
                            {
                                Delagados InvocarNombre = new Delagados(LlamadoMedBinario.CompareToNombre);
                                Singleton.Instance.AccesoArbol.Add(IngresoArbol , InvocarNombre);
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
            Singleton.Instance.NuevaListaCliente.Clear();
            InventarioMedicina InfoTotalMedicina = new InventarioMedicina();
            if (SSearch!=null)
            {
                DelegadoString InvocarNombreuscar = new DelegadoString(LlamadoMedBinario.CompareString);
                MedicinasBinario Buscado = Singleton.Instance.AccesoArbol.Buscar(SSearch, InvocarNombreuscar);
                if (Buscado == default)
                {
                    //Mensaje que no lo encontro porque la cantidad de medicinas de ese nombre es 0
                }
                else 
                {
                    InfoTotalMedicina = Buscado.Posicion.Data;
                    InventarioMedicina NuevoJuga = new InventarioMedicina
                    {
                        NombreMedicina = InfoTotalMedicina.NombreMedicina,
                        Id = InfoTotalMedicina.Id,
                        Descripcion = InfoTotalMedicina.Descripcion,
                        CasaProductora = InfoTotalMedicina.CasaProductora,
                        Precio = InfoTotalMedicina.Precio,
                        Existencia = InfoTotalMedicina.Existencia
                    };
                    Singleton.Instance.NuevaListaCliente.Add(NuevoJuga);
                }
            }
            
            //Mostrar InfoTotalMedicina si fue encontrado
            return View(Singleton.Instance.NuevaListaCliente);
        }
        public IActionResult Agregar(int SExistencia) 
        {
            ViewData["CurrentSExistencia"] = SExistencia;

            return RedirectToAction("AgregarBuscarMedicina"); 
        }
        public IActionResult ListaCarrito()
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
