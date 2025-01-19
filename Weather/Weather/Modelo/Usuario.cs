using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Modelo
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre {  get; set; }
        public string Pass { get; set; }
        public string Rol {  get; set; }

        public Usuario(int i,string n, string p, string r) {
            this.Id= i;
            this.Nombre = n;
            this.Pass = p;
            this.Rol = r;
        }
    }
}
