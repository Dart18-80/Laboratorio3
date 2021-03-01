using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriadeClasesED
{
    class NodoBinario <T>
    {
        public NodoBinario<T> Derecha { get; set; }
        public NodoBinario<T> Izquierda { get; set; }

        public T Data { get; set; }

    }
}
