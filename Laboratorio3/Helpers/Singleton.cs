using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratorio3.Models;
using LibreriadeClasesED;

namespace Laboratorio3.Helpers
{
    public class Singleton
    {
        private static Singleton _instance = null;
        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }
        public DoubleList<InventarioMedicina> ListaMedicina = new DoubleList<InventarioMedicina>();
        public DoubleList<NodoCarrito> ListaCarrito = new DoubleList<NodoCarrito>();
        public ArbolBinario<MedicinasBinario> AccesoArbol = new ArbolBinario<MedicinasBinario>();
        public Nodo<InventarioMedicina> Procedimiento = new Nodo<InventarioMedicina>();
        public List<InventarioMedicina> Nueva = new List<InventarioMedicina>();
        public List<InventarioMedicina> NuevaListaCliente = new List<InventarioMedicina>();
        public List<Cliente> ListaCliente = new List<Cliente>();
        public List<InventarioMedicina> ListExistencia = new List<InventarioMedicina>();
        public List<NodoCarrito> Carrito = new List<NodoCarrito>();
        public int contadorCero;

    }
}
