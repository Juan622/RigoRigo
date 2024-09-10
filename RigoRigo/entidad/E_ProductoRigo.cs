using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class E_ProductoRigo
    {
        public int Id { get; set; }
        public string Articulo { get; set; }
        public decimal ValorUnitario { get; set; }
        public int CantidadExistente { get; set; }
    }
}
