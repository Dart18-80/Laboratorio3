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
        public static double SumaTotal = 0;
        public static string medicamentostotales = null;

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

        delegate int DelegadoInventario(InventarioMedicina Med1, string nombre);

        delegate int Delagado(InventarioMedicina Med);//llamar delegado

        MedicinasBinario LlamadoMedBinario = new MedicinasBinario();
        InventarioMedicina LLamadoInventario = new InventarioMedicina();

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
                            Nodo<InventarioMedicina> Direccion = Singleton.Instance.ListaMedicina.AddHead(Nuevo);
                            MedicinasBinario IngresoArbol = new MedicinasBinario
                            {
                                Nombre = Convert.ToString(result[3]).Replace('"', ' '),
                                Posicion = Direccion
                            };
                            if (Convert.ToInt32(result[11])!=0)
                            {
                                Delagados InvocarNombre = new Delagados(LlamadoMedBinario.CompareToNombre);
                                Singleton.Instance.AccesoArbol.Add(IngresoArbol , InvocarNombre);
                                Singleton.Instance.contadorCero++;
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
            Singleton.Instance.Procedimiento.Mostrar(Singleton.Instance.ListaMedicina.Header, Singleton.Instance.Nueva);

            return View(Singleton.Instance.Nueva);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Abastecer()//Abastecer los medicamnetos iguales a 0, a un numero random
        {
            Singleton.Instance.Nueva.Clear();
            try
            {
            while (Singleton.Instance.contadorCero == 0) {
                DelegadoInventario InvocarExistencia = new DelegadoInventario(LLamadoInventario.CompareExistencia);
                InventarioMedicina NodoBuscado = Singleton.Instance.ListaMedicina.Buscar(Singleton.Instance.ListaMedicina.Header, "0", InvocarExistencia);

                Random rnd = new Random();
                int numerorand = rnd.Next(1, 15);
                NodoBuscado.Existencia = numerorand;
                Nodo<InventarioMedicina> Direccion = Singleton.Instance.ListaMedicina.AddHead(NodoBuscado);
                MedicinasBinario IngresoArbol = new MedicinasBinario
                {
                    Nombre = NodoBuscado.NombreMedicina,
                    Posicion = Direccion
                };
                Delagados InvocarNombre = new Delagados(LlamadoMedBinario.CompareToNombre);
                Singleton.Instance.AccesoArbol.Add(IngresoArbol, InvocarNombre);
                Singleton.Instance.contadorCero--;
            }
            return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }
        public IActionResult IngresoPedido()
        {
            return View();
        }
        [HttpPost]
        public IActionResult IngresoPedido(IFormCollection collection, string IddeNit)
        {
            ViewData["CurrentNit"] = IddeNit;
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
                MedicinasBinario Buscado = Singleton.Instance.AccesoArbol.BuscarABB(SSearch, InvocarNombreuscar);
                if (Buscado == default)
                {
                    DelegadoInventario delegadoInventario = new DelegadoInventario(LLamadoInventario.CompareName);
                    InventarioMedicina NodoCantidad0 = Singleton.Instance.ListaMedicina.Buscar(Singleton.Instance.ListaMedicina.Header,SSearch, delegadoInventario);
                    Singleton.Instance.NuevaListaCliente.Add(NodoCantidad0);
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
        public IActionResult Agregar(int SExistencia, string SNombre) //donde se agregan los nodos a una lista para mostrar en la lista carrito
        {
            ViewData["CurrentSExistencia"] = SExistencia;

            NodoCarrito NuevoPedido = new NodoCarrito { Nombre = SNombre, Cantidad = SExistencia };

            Singleton.Instance.ListaCarrito.AddHead(NuevoPedido);

            return RedirectToAction("AgregarBuscarMedicina"); 
        }
        public IActionResult AceptarCarrito()//codigo donde se aceptan los cambios del carrito
        {
            foreach (NodoCarrito Cambios in Singleton.Instance.Carrito) 
            {
                DelegadoString InvocarNombreuscar = new DelegadoString(LlamadoMedBinario.CompareString);
                MedicinasBinario Buscado = Singleton.Instance.AccesoArbol.BuscarABB(Cambios.Nombre, InvocarNombreuscar);

                InventarioMedicina NodoBuscado = Buscado.Posicion.Data;

                InventarioMedicina NuevoNodo = NodoBuscado;
                NuevoNodo.Existencia = NuevoNodo.Existencia - Cambios.Cantidad;

                DelegadoInventario delegadoInventario = new DelegadoInventario(LLamadoInventario.CompareName);

                Singleton.Instance.ListaMedicina.Modificar(Singleton.Instance.ListaMedicina.Header,Cambios.Nombre, NuevoNodo, delegadoInventario);
               
                if (medicamentostotales != null)
                {
                    medicamentostotales += "," + Cambios.Nombre;
                }
                else
                {
                    medicamentostotales = Cambios.Nombre;
                }
                string a = Buscado.Posicion.Data.Precio.Replace("$", string.Empty);
                double precio = Convert.ToDouble(a.ToString());
                SumaTotal += Cambios.Cantidad * precio;
                if (medicamentostotales!=null)
                {
                    medicamentostotales +=","+Cambios.Nombre;
                }
                else
                {
                    medicamentostotales = Cambios.Nombre;
                }
                if (NuevoNodo.Existencia <= 0) 
                {
                    DelegadoString ComparacionBorrar = new DelegadoString(LlamadoMedBinario.CompareString);
                    Singleton.Instance.AccesoArbol.EliminarDeArbol1(Buscado.Nombre , ComparacionBorrar);
                    if (NuevoNodo.Existencia == 0) 
                    {
                        Singleton.Instance.contadorCero++;
                    }
                }
            }
            return RedirectToAction("OrdenCliente");
        }
        public IActionResult ListaCarrito()//mostrar carrito
        {
            Singleton.Instance.Carrito.Clear();
            Singleton.Instance.Carrito = Singleton.Instance.ListaCarrito.Mostrar(Singleton.Instance.ListaCarrito.Header, Singleton.Instance.Carrito);
            return View(Singleton.Instance.Carrito);
        }

        public IActionResult OrdenCliente() 
        {
            string Nombre = Singleton.Instance.ListaCliente[Cliente.cont - 1].NombreCliente;
            string Direccion = Singleton.Instance.ListaCliente[Cliente.cont - 1].DireccionCliente;
            string Nit = Singleton.Instance.ListaCliente[Cliente.cont - 1].Nit;


            ViewData["Nombre"] = Nombre;
            ViewData["Direccion"] = Direccion;
            ViewData["Nit"] = Nit;
            ViewData["Medi"] = medicamentostotales;
            ViewData["Total"] = SumaTotal;
            medicamentostotales = null;
            SumaTotal = 0;
            return View() ;

        }
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
