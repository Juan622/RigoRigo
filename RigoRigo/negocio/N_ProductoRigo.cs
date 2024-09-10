using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class N_ProductoRigo
    {
        D_ProductoRigo objDato = new D_ProductoRigo();

        public DataTable ListaProductos()
        {
            return objDato.VerProductoSP();
        }
    }
}
