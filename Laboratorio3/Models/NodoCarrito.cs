using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio3.Models
{
    public class NodoCarrito : IComparable
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

        public int CompareString(NodoCarrito obj, InventarioMedicina obj2)
        {
            return obj.Nombre.CompareTo(obj2.NombreMedicina);
        }
        public int CompareTo(object obj)
        {
            if (Convert.ToInt16(this.CompareTo(obj)) > 0)
                return 1;
            else if (Convert.ToInt16(this.CompareTo(obj)) < 0)
                return -1;
            else
                return 0;
        }
    }
}
