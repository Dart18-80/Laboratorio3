using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio3.Models
{
    public class Cliente: IComparable
    {
    public string NombreCliente { get; set; }
    public string DireccionCliente { get; set; }
    public string Nit { get; set; }
    public string ListadoFarmacos{ get; set; }
    public double TotalPrecio { get; set; }
        public int Idcliente;
        public static int cont = 0;
        public int CompareTo(object obj)
    {
        throw new NotImplementedException();
    }
}
}
