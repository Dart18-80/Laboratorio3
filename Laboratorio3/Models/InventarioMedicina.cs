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
        public int CompareByName(InventarioMedicina Medicamento1, string Medicamento2)
        {
            return Medicamento1.NombreMedicina.CompareTo(Medicamento2);
        }
    }
}
