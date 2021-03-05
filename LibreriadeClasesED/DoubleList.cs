using System;
using System.Collections.Generic;
using System.Text;
using LibreriadeClasesED;
 

namespace LibreriadeClasesED
{
    public class DoubleList<T> where T : IComparable
    {
        public Nodo<T> Header { get; set; }
        public Nodo<T> Tail { get; set; }

        List<T> DataNode = new List<T>();

        public DoubleList()
        {
            Header = null;
            Tail = null;
        }

        public Nodo<T> AddHead(T data)
        {
            if (Header == null)
            {
                Header = new Nodo<T>() { Data = data };
                Tail = Header;
                return Header;
            }
            else
            {
                var oldHead = Header;
                Header = new Nodo<T>()
                {
                    Data = data,
                    Next = oldHead
                };
                oldHead.Previous = Header;
                return oldHead.Previous;
            }
        }
        public List<T> Mostrar(Nodo<T> Cabeza)
        {
            if (Cabeza != null)
            {
                DataNode.Add(Cabeza.Data);
                Mostrar(Cabeza.Next);
                return null;
            }
            else
                return DataNode;

        }
        public void ReabastecerMedicamentos(Nodo<T> cabeza, Delegate Condicion) 
        {
            if (cabeza!=null)
            {
                if (cabeza.Data!=null)
                {
                }
                else
                {
                    ReabastecerMedicamentos(cabeza.Next, Condicion);
                }
            }
        }
        public T Buscar(Nodo<T> Cabeza, string FoundNodo, Delegate Condicion)
        {
            if (Cabeza == null)
            {
                return default;
            }
            else
            {

                if (Convert.ToInt16(Condicion.DynamicInvoke(Cabeza.Data, FoundNodo)) == 0)
                {
                    return Cabeza.Data;
                }
                else
                {
                    Buscar(Cabeza.Next, FoundNodo, Condicion);
                    return default;
                }
            }
        }
    }
}
