using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriadeClasesED
{
    public class NodoBinario <T>
    {
        public NodoBinario<T> Derecha { get; set; }
        public NodoBinario<T> Izquierda { get; set; }
        public NodoBinario<T> Padre { get; set; }
        public T Data { get; set; }
        public int Altura { get; set; }
        public int Index { get; set; }

    }
}
