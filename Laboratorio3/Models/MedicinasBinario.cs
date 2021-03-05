using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibreriadeClasesED;

namespace Laboratorio3.Models
{
    public class MedicinasBinario : IComparable
    {
        public string Nombre { get; set; }
        public Nodo<InventarioMedicina> Posicion { get; set; }    

        public int CompareToNombre(MedicinasBinario obj, MedicinasBinario obj2)
        {
            return obj.Nombre.CompareTo(obj2.Nombre);
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
