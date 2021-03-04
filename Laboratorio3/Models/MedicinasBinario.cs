using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio3.Models
{
    public class MedicinasBinario : IComparable
    {
        public string Nombre { get; set; }
        public int Posicion { get; set; }

        public int CompareTo(MedicinasBinario Medicamento1, MedicinasBinario other, Delegate Condicion)
        {
            return Convert.ToInt32(Condicion.DynamicInvoke(Medicamento1, other));
        }
        public int CompareyName(MedicinasBinario Medicamento1, string Medicamento2)
        {
            return Medicamento1.Nombre.CompareTo(Medicamento2);
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
