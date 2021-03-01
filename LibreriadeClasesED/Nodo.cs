using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriadeClasesED
{
    public class Nodo<T>
    {
        public Nodo<T> Previous { get; set; }
        public Nodo<T> Next { get; set; }

        public T Data { get; set; }

        public List<T> Mostrar(Nodo<T> Cabeza, List<T> Regreso)
        {
            if (Cabeza != null)
            {
                Regreso.Add(Cabeza.Data);
                Mostrar(Cabeza.Next, Regreso);
                return null;
            }
            else
                return Regreso;
        }
        public List<T> MostrarNodo(T DataView, List<T> Regreso)
        {
            Regreso.Add(DataView);
            return Regreso;
        }
    }
}
