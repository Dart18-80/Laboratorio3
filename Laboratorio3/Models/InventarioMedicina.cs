using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibreriadeClasesED;

namespace Laboratorio3.Models
{
    public class InventarioMedicina : IComparable
    {
        public string Id { get; set; }
        public string NombreMedicina { get; set; }
        public string Descripcion { get; set; }
        public string CasaProductora { get; set; }
        public string Precio { get; set; }
        public int Existencia { get; set; }

        public IFormFile FileC { get; set; }

        public int CompareTo(InventarioMedicina Medicamento1, InventarioMedicina other, Delegate Condicion)
        {
            return Convert.ToInt32(Condicion.DynamicInvoke(Medicamento1, other));
        }
        public int CompareNameMedi(InventarioMedicina Medicamento1, string Medicamento2)
        {
            return Medicamento1.NombreMedicina.CompareTo(Medicamento2);
        }
        public int CompareExist(InventarioMedicina MedicinaExist)
        {
            return MedicinaExist.Existencia.CompareTo(0);
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
