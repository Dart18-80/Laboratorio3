using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriadeClasesED
{
    class ArbolBinario <T> where T : IComparable
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
                if (Padre.Izquierda == null)
                {
                    Padre.Izquierda.Data = data;
                }
                else 
                {
                    Insertar(Padre.Izquierda, data, Comparacion);
                }
            }
            else if (Convert.ToInt16(Comparacion.DynamicInvoke(Padre.Data, data)) > 0)
            {
                if (Padre.Derecha == null)
                {
                    Padre.Derecha.Data = data;
                }
                else
                {
                    Insertar(Padre.Derecha, data, Comparacion);
                }
            }
            else 
            {

            }
        }

        public void Insertar(NodoBinario<T> Nuevo ,T data, Delegate Comparacion) 
        {
            if (Convert.ToInt16(Comparacion.DynamicInvoke(Nuevo.Data, data)) < 0)
            {
                if (Nuevo.Izquierda == null)
                {
                    Nuevo.Izquierda.Data = data;
                }
                else
                {
                    Insertar(Nuevo.Izquierda, data, Comparacion);
                }
            }
            else if (Convert.ToInt16(Comparacion.DynamicInvoke(Nuevo.Data, data)) > 0)
            {
                if (Nuevo.Derecha == null)
                {
                    Nuevo.Derecha.Data = data;
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

        public void Delete(NodoBinario<T> Llamado , T NodoDelete, Delegate Comparacion) 
        {
            if (Llamado == null)
            {
                // No hace nada
            }
            else 
            {
                if (Convert.ToInt16(Comparacion.DynamicInvoke(Llamado.Data, NodoDelete)) == 0)
                {
                    Eliminar(Llamado);
                }
                else if (Convert.ToInt16(Comparacion.DynamicInvoke(Llamado.Data, NodoDelete)) > 0)
                {
                    Delete(Llamado.Derecha, NodoDelete, Comparacion);
                }
                else if((Convert.ToInt16(Comparacion.DynamicInvoke(Llamado.Data, NodoDelete)) < 0))
                {
                    Delete(Llamado.Izquierda, NodoDelete, Comparacion);
                }
            }
        }

        public void Eliminar(NodoBinario<T> Eliminado) 
        {
            if (Eliminado.Izquierda != null || Eliminado.Derecha != null)
            {
                NodoBinario<T> Helper = Eliminado.Derecha;
                Eliminado = MasDerecha(Eliminado.Izquierda);
                Eliminado.Derecha = Helper;
            }
            else if (Eliminado.Izquierda != null || Eliminado.Derecha == null)
            {

                Eliminado = MasDerecha(Eliminado.Izquierda);
            }
            else if (Eliminado.Izquierda == null || Eliminado.Derecha != null)
            {
                Eliminado = MasIzquierda(Eliminado.Derecha);
            }
            else
            {
                Eliminado = null;
            }
        }

        public NodoBinario<T> MasIzquierda(NodoBinario<T> Cambiar) 
        {
            if (Cambiar.Izquierda != null)
            {
                MasDerecha(Cambiar.Izquierda);
                return null;
            }
            else
            {
                return Cambiar;
            }
        }

        public NodoBinario<T> MasDerecha(NodoBinario<T> Cambiar)
        {
            if (Cambiar.Derecha != null)
            {
                MasDerecha(Cambiar.Derecha);
                return null;
            }
            else 
            {
                return Cambiar;
            }
        }
    }
}
