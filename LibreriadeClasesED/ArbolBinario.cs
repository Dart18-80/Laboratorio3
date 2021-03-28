using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriadeClasesED
{
    public class ArbolBinario<T> where T : IComparable
    {
        public NodoBinario<T> Raiz { get; set; }

        public ArbolBinario()
        {
            Raiz = null;
        }
        public void RaízC()
        {
            Raiz = null;
        }
        public void Add(T data, Delegate Comparacion)
        {
            NodoBinario<T> nuevo = new NodoBinario<T>();
            nuevo.Data = data;
            nuevo.Izquierda = null;
            nuevo.Derecha = null;
            nuevo.Padre = null;
            nuevo.Altura = 1;
            if (Raiz == null)
                Raiz = nuevo;
            else
            {
                Insertar(nuevo, Raiz, Comparacion);
            }
        }
        public void Insertar(NodoBinario<T> Nuevo, NodoBinario<T> n, Delegate Comparacion)
        {
            if (n != null)
            {
                int compar = Convert.ToInt16(Comparacion.DynamicInvoke(Nuevo.Data, n.Data));
                if (compar == 0)
                {

                }
                else if (compar > 0)
                {
                    if (n.Derecha != null)
                    {
                        Insertar(Nuevo, n.Derecha, Comparacion);
                        n.Altura = Mayor(n.Derecha, n.Izquierda) + 1;
                    }
                    else
                    {
                        n.Derecha = Nuevo;
                        Nuevo.Padre = n;
                        n.Altura = Mayor(n.Derecha, n.Izquierda) + 1;
                    }
                }
                else
                {
                    if (n.Izquierda != null)
                    {
                        Insertar(Nuevo, n.Izquierda, Comparacion);
                        n.Altura = Mayor(n.Derecha, Nuevo.Izquierda) + 1;
                    }
                    else
                    {
                        n.Izquierda = Nuevo;
                        Nuevo.Padre = n;
                        n.Altura = Mayor(n.Derecha, n.Izquierda) + 1;
                    }
                }
            }
            int balanceo = Balanceo(n);
            if (balanceo > 1)
            {
                balanceo = Balanceo(n.Derecha);
                if (balanceo == -1)
                {
                    RotacionDer(n.Derecha);
                }
                RotacioIzq(n);
            }
            else if (balanceo < -1)
            {
                balanceo = Balanceo(n.Izquierda);
                if (balanceo == 1)
                {
                    RotacioIzq(n.Izquierda);
                }
                RotacionDer(n);
            }
        }

        public int Balanceo(NodoBinario<T> N)
        {
            if (N.Derecha != null && N.Izquierda != null)
            {
                return (N.Derecha.Altura - N.Izquierda.Altura);
            }
            else if (N.Derecha != null)
            {
                return N.Derecha.Altura;
            }
            else if (N.Izquierda != null)
            {
                return 0 - N.Izquierda.Altura;
            }
            else
            {
                return 0;
            }
        }

        public int Mayor(NodoBinario<T> a, NodoBinario<T> b)
        {
            if (a != null && b != null)
            {
                if (a.Altura > b.Altura)
                {
                    return (a.Altura);
                }
                else
                {
                    return (b.Altura);
                }
            }
            else if (a != null)
            {
                return (a.Altura);
            }
            else if (b != null)
            {
                return (b.Altura);
            }
            else
            {
                return 0;
            }
        }
        public int BuscarUno(string nBuscar, NodoBinario<T> N, Delegate Comparacion)
        {
            int NResult = -1;
            int Verificacion = Convert.ToInt16(Comparacion.DynamicInvoke(nBuscar, Raiz.Data));
            if (Verificacion == 0)
            {
                int compar = Convert.ToInt16(Comparacion.DynamicInvoke(nBuscar, Raiz.Data));
                if (compar < 0)
                    if (N.Izquierda != null)
                    {
                        NResult = BuscarUno(nBuscar, Raiz.Izquierda, Comparacion);
                    }
                    else
                        if (N.Derecha != null)
                    {
                        NResult = BuscarUno(nBuscar, Raiz.Derecha, Comparacion);
                    }
            }
            else
            {
                NResult = Raiz.Index;
            }
            return NResult;
        }
        public int Buscar(string nBuscar, Delegate Comparacion)
        {
            int NResult = -1;
            int Verificacion = Convert.ToInt16(Comparacion.DynamicInvoke(nBuscar, Raiz.Data));
            if (Verificacion == 0)
            {
                int compar = Convert.ToInt16(Comparacion.DynamicInvoke(nBuscar, Raiz.Data));
                if (compar < 0)
                    if (Raiz.Izquierda != null)
                    {
                        NResult = BuscarUno(nBuscar, Raiz.Izquierda, Comparacion);
                    }
                    else
                        if (Raiz.Derecha != null)
                    {
                        NResult = BuscarUno(nBuscar, Raiz.Derecha, Comparacion);
                    }
            }
            else
            {
                NResult = Raiz.Index;
            }
            return NResult;
        }

        public T BuscarEUno(string nBuscar, NodoBinario<T> Ant, NodoBinario<T> Estar, Delegate Comparacion)
        {
            NodoBinario<T> Nresult = null;
            int Verificacion = Convert.ToInt16(Comparacion.DynamicInvoke(Nresult, Estar.Data));
            if (Verificacion == 0)
            {
                int compar = Convert.ToInt16(Comparacion.DynamicInvoke(Nresult, Estar.Data));
                if (compar < 0)
                {
                    if (Estar.Izquierda != null)
                    {
                        Nresult.Data = BuscarEUno(nBuscar, Estar, Estar.Izquierda, Comparacion);
                    }
                }
                else
                {
                    if (Estar.Derecha != null)
                    {
                        Nresult.Data = BuscarEUno(nBuscar, Estar, Estar.Derecha, Comparacion);
                    }
                }
            }
            else
            {
                Nresult = Estar;
            }
            return Nresult.Data;
        }

        public T BuscarE(string nBuscar, NodoBinario<T> N, Delegate Comparacion)
        {
            NodoBinario<T> Nresult = null;
            int Vereficacion = Convert.ToInt16(Comparacion.DynamicInvoke(nBuscar, N.Data));
            if (Vereficacion == 0)
            {
                int compar = Convert.ToInt16(Comparacion.DynamicInvoke(nBuscar, N.Data));
                if (compar < 0)
                {
                    if (N.Izquierda != null)
                    {
                        Nresult.Data = BuscarE(nBuscar, N.Izquierda, Comparacion);
                    }
                }
                else
                {
                    if (N.Derecha != null)
                    {
                        Nresult.Data = BuscarE(nBuscar, N.Derecha, Comparacion);
                    }
                }
            }
            else
            {
                Nresult = N;
            }
            return Nresult.Data;
        }

        public void EliminarUno(T nEliminar, NodoBinario<T> n, Delegate Comparacion)
        {
            int compar = Convert.ToInt16(Comparacion.DynamicInvoke(nEliminar, n.Data));
            if (compar == 0)
            {
                Eliminar(n);
            }
            else if (compar < 0)
            {
                if (n.Izquierda != null)
                {
                    EliminarUno(nEliminar, n.Izquierda, Comparacion);
                }
            }
            else
            {
                if (n.Derecha != null)
                {
                    EliminarUno(nEliminar, n.Derecha, Comparacion);
                }
            }
            int balanceo = Balanceo(n);
            if (balanceo > 1)
            {
                balanceo = Balanceo(n.Derecha);
                if (balanceo == -1)
                {
                    RotacionDer(n.Derecha);
                }
                RotacioIzq(n);
            }
            else if (balanceo < -1)
            {
                balanceo = Balanceo(n.Izquierda);
                if (balanceo == 1)
                {
                    RotacioIzq(n.Izquierda);
                }
                RotacionDer(n);
            }
        }

        public void Eliminar(NodoBinario<T> N)
        {
            if (N.Derecha == null && N.Izquierda == null)
            {
                if (N.Padre.Derecha == N)
                {
                    N.Padre.Derecha = null;
                }
                else
                {
                    N.Padre.Izquierda = null;
                }
            }
            else if (N.Derecha != null && N.Izquierda != null)
            {
                NodoBinario<T> Masizq = MIzquierda(N);
                if (N.Padre == null)
                {
                    Raiz = Masizq;
                    Masizq.Derecha = N.Derecha;
                }
                else
                {
                    if (N.Padre.Derecha == null)
                    {
                        N.Padre.Derecha = Masizq;
                        Masizq.Derecha = N.Derecha;
                    }
                    else
                    {
                        N.Padre.Izquierda = Masizq;
                        Masizq.Derecha = N.Derecha;
                    }
                }
            }
            else if (N.Derecha != null)
            {
                if (N.Padre.Derecha == N)
                {
                    N.Padre.Derecha = N.Derecha;
                }
                else
                {
                    N.Padre.Izquierda = N.Derecha;
                }
            }
            else
            {
                if (N.Padre.Derecha == N)
                {
                    N.Padre.Derecha = N.Izquierda;
                }
                else
                {
                    N.Padre.Izquierda = N.Izquierda;
                }
            }
        }

        public NodoBinario<T> MIzquierda(NodoBinario<T> N)
        {
            if (N.Izquierda == null)
            {
                Eliminar(N);
                return N;
            }
            else
            {
                return MIzquierda(N.Izquierda);
            }
        }

        public void RotacioIzq(NodoBinario<T> N)
        {
            if (N.Padre != null)
            {
                bool Pder;
                if (N.Padre.Derecha == N)
                {
                    Pder = true;
                    N.Padre.Derecha = N.Derecha;
                }
                else
                {
                    Pder = false;
                    N.Padre.Izquierda = N.Derecha;
                }
                N.Derecha.Padre = N.Padre;
                N.Derecha = N.Derecha.Izquierda;
                if (N.Derecha != null)
                {
                    N.Derecha.Padre = N;
                }
                if (Pder)
                {
                    N.Padre.Derecha.Izquierda = N;
                    N.Padre = N.Padre.Derecha;
                }
                else
                {
                    N.Padre.Izquierda.Izquierda = N;
                    N.Padre = N.Padre.Izquierda;
                }
            }
            else
            {
                N.Derecha.Padre = N.Padre;
                N.Padre = N.Derecha;
                N.Derecha = N.Derecha.Izquierda;
                N.Padre.Izquierda = N;
                if (N.Derecha != null)
                    N.Derecha.Padre = N;
                if (N == Raiz)
                    Raiz = N.Padre;
            }
            N.Altura = Mayor(N.Izquierda, N.Izquierda) + 1;
            N.Padre.Altura = Mayor(N.Padre.Izquierda, N.Padre.Derecha) + 1;
        }

        public void RotacionDer(NodoBinario<T> N)
        {
            if (N.Padre != null)
            {
                bool Pder;
                if (N.Padre.Derecha==N)
                {
                    Pder = true;
                    N.Padre.Derecha = N.Izquierda;
                }
                else
                {
                    Pder = false;
                    N.Padre.Izquierda = N.Izquierda;
                }
                N.Izquierda.Padre = N.Padre;
                N.Izquierda = N.Izquierda.Derecha;
                if (N.Izquierda !=null)
                {
                    N.Izquierda.Padre = N;
                }
                if (Pder)
                {
                    N.Padre.Derecha.Derecha = N;
                    N.Padre = N.Padre.Derecha;
                }
                else
                {
                    N.Padre.Izquierda.Derecha = N;
                    N.Padre = N.Padre.Izquierda;
                }
            }
            else
            {
                N.Izquierda.Padre = N.Padre;
                N.Padre = N.Izquierda;
                N.Izquierda = N.Izquierda.Derecha;
                N.Padre.Derecha = N;
                if (N.Izquierda != null)
                    N.Izquierda.Padre = N;
                if (N == Raiz)
                    Raiz = N.Padre;
            }
            N.Altura = Mayor(N.Izquierda, N.Derecha) + 1;
            N.Padre.Altura = Mayor(N.Padre.Izquierda, N.Padre.Derecha) + 1; ;
        }

        //Buscar ABB Lab 2
        public T BuscarABB(string Nombre, Delegate Comparacion) 
        {
            if (Raiz == null)
            {
                return default;
            }
            else
            {
                T Igualar;

                if (Convert.ToInt16(Comparacion.DynamicInvoke(Raiz.Data, Nombre)) == 0)
                {
                    Igualar = Raiz.Data;
                }
                else if (Convert.ToInt16(Comparacion.DynamicInvoke(Raiz.Data, Nombre)) < 0)
                {
                    Igualar = BuscarNodoABB(Raiz.Derecha, Nombre, Comparacion);
                }
                else if (Convert.ToInt16(Comparacion.DynamicInvoke(Raiz.Data, Nombre)) > 0)
                {
                    Igualar = BuscarNodoABB(Raiz.Izquierda, Nombre, Comparacion);
                }
                else
                {
                    Igualar = default;
                }
                return Igualar;
            }
        }

        public T BuscarNodoABB(NodoBinario<T> Hijos, string Nombre, Delegate Comparacion)
        {
            if (Hijos == null)
            {
                return default;
            }
            else
            {
                T Igualar;
                if (Convert.ToInt16(Comparacion.DynamicInvoke(Hijos.Data, Nombre)) == 0)
                {
                    Igualar = Hijos.Data;
                }
                else if (Convert.ToInt16(Comparacion.DynamicInvoke(Hijos.Data, Nombre)) < 0)
                {
                    Igualar = BuscarNodoABB(Hijos.Derecha, Nombre, Comparacion);
                }
                else if (Convert.ToInt16(Comparacion.DynamicInvoke(Hijos.Data, Nombre)) > 0)
                {
                    Igualar = BuscarNodoABB(Hijos.IZ, Nombre, Comparacion);
                }
                else
                {
                    Igualar = default;
                }
                return Igualar;
            }
        }
    }
}
