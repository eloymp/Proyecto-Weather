using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Controlador;

namespace Weather.Singleton
{
    public class GlobalData
    {
        private static GlobalData _instance;
        public static GlobalData Instance => _instance ??= new GlobalData();

        public BBDD miBBDD = new BBDD();

        // Constructor privado para evitar creación directa
        private GlobalData() { }
    }
}
