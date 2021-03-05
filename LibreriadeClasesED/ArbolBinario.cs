using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriadeClasesED
{
    public class ArbolBinario <T> where T : IComparable
    {
        public NodoBinario<T> Padre { get; set; }

        public ArbolBinario()
        {
            Padre = null;
        }

        public void Add(T data, Delegate Comparacion) 
        {
            if (Padre == null)
            {
                Padre = new NodoBinario<T>() { Data = data };
            }
            else if (Convert.ToInt16(Comparacion.DynamicInvoke(Padre.Data, data)) < 0)
            {
                if (Padre.Derecha == null)
                {
                    NodoBinario<T> NuevoNodo = new NodoBinario<T> { Data = data };
                    Padre.Derecha = NuevoNodo;
                }
                else 
                {
                    Insertar(Padre.Derecha, data, Comparacion);
                }
            }
            else if (Convert.ToInt16(Comparacion.DynamicInvoke(Padre.Data, data)) > 0)
            {
                if (Padre.Izquierda == null)
                {
                    NodoBinario<T> NuevoNodo = new NodoBinario<T> { Data = data };
                    Padre.Izquierda = NuevoNodo;
                }
                else
                {
                    Insertar(Padre.Izquierda, data, Comparacion);
                }
            }
            else 
            {

            }
        }

        public void Insertar(NodoBinario<T> Nuevo ,T data, Delegate Comparacion) 
        {
            if (Convert.ToInt16(Comparacion.DynamicInvoke(Nuevo.Data, data)) > 0)
            {
                if (Nuevo.Izquierda == null)
                {
                    NodoBinario<T> NuevoNodo = new NodoBinario<T> { Data = data };
                    Nuevo.Izquierda = NuevoNodo;
                }
                else
                {
                    Insertar(Nuevo.Izquierda, data, Comparacion);
                }
            }
            else if (Convert.ToInt16(Comparacion.DynamicInvoke(Nuevo.Data, data)) < 0)
            {
                if (Nuevo.Derecha == null)
                {
                    NodoBinario<T> NuevoNodo = new NodoBinario<T> { Data = data };
                    Nuevo.Derecha = NuevoNodo;
                }
                else
                {
                    Insertar(Nuevo.Derecha, data, Comparacion);
                }
            }
            else 
            {
            }
        }

        public void Eliminar(string Eliminado, Delegate Comparacion) 
        {
            if (Padre != null) 
            {
                if (Convert.ToInt16(Comparacion.DynamicInvoke(Padre.Data, Eliminado)) == 0)
                {
                    if (Padre.Derecha != null || Padre.Izquierda != null)
                    {
                        Padre.Data = MasDerecha(Padre, Padre.Izquierda);
                    }
                    else if (Padre.Izquierda != null)
                    {
                        Padre.Data = MasDerecha(Padre, Padre.Izquierda);
                    }
                    else if (Padre.Derecha != null)
                    {
                        Padre.Data = MasIzquierda(Padre, Padre.Derecha);
                    }
                    else
                    {
                        Padre = null;
                    }
                }
                else if (Convert.ToInt16(Comparacion.DynamicInvoke(Padre.Data, Eliminado)) < 0)
                {
                    if (Padre.Derecha != null)
                    {
                        Delete(Padre, Padre.Derecha, Eliminado, Comparacion);
                    }
                }
                else if (Convert.ToInt16(Comparacion.DynamicInvoke(Padre.Data, Eliminado)) > 0)
                {
                    if (Padre.Izquierda != null)
                    {
                        Delete(Padre, Padre.Izquierda, Eliminado, Comparacion);
                    }
                }
                else 
                {
                    //No se encontro el nodo
                }
            }
        }

        public void Delete(NodoBinario<T> Origen, NodoBinario<T> Siguiente, string Eliminado, Delegate Condicion) 
        {
            if (Convert.ToInt16(Condicion.DynamicInvoke(Siguiente.Data, Eliminado)) == 0)
            {
                if (Siguiente.Derecha != null || Siguiente.Izquierda != null)
                { 
                    Siguiente.Data = MasDerecha(Siguiente, Siguiente.Izquierda);
                }
                else if (Siguiente.Izquierda != null)
                {
                    Siguiente.Data = MasDerecha(Siguiente, Siguiente.Izquierda);
                }
                else if (Siguiente.Derecha != null)
                {
                    Siguiente.Data = MasIzquierda(Siguiente, Siguiente.Derecha);
                }
                else
                {
                    if (Convert.ToInt16(Condicion.DynamicInvoke(Origen.Data, Eliminado)) < 0)
                    {
                        Origen.Derecha = null;
                    }
                    else 
                    {
                        Origen.Izquierda = null;
                    }
                }
            }
            else if (Convert.ToInt16(Condicion.DynamicInvoke(Siguiente.Data, Eliminado)) < 0)
            {
                if (Siguiente.Derecha != null)
                {
                    Delete(Siguiente, Siguiente.Derecha, Eliminado, Condicion);
                }
            }
            else if (Convert.ToInt16(Condicion.DynamicInvoke(Siguiente.Data, Eliminado)) > 0) 
            {
                if (Siguiente.Izquierda != null)
                {
                    Delete(Siguiente, Siguiente.Izquierda, Eliminado, Condicion);
                }
            }
        }

        public T MasDerecha(NodoBinario<T> Origen, NodoBinario<T> Siguiente) 
        {
            if (Siguiente.Derecha != null)
            {
                MasDerecha(Siguiente, Siguiente.Derecha);
                return default;
            }
            else 
            {
                Origen.Derecha = null;
                return Siguiente.Data;
            }
        }
        public T MasIzquierda(NodoBinario<T> Origen, NodoBinario<T> Siguiente)
        {
            if (Siguiente.Izquierda != null)
            {
                MasDerecha(Siguiente, Siguiente.Izquierda);
                return default;
            }
            else
            {
                Origen.Izquierda = null;
                return Siguiente.Data;
            }
        }

        public T EnviarDentro() 
        {
            return Padre.Data;
        }
    }
}
