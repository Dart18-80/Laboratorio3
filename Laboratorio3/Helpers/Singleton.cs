using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratorio3.Models;
using LibreriadeClasesED;

namespace Laboratorio3.Helpers
{
    public class Singleton
    {
        private static Singleton _instance = null;
        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }


        public ArbolBinario<MedicinasBinario> AccesoArbol = new ArbolBinario<MedicinasBinario>();
    }
}
