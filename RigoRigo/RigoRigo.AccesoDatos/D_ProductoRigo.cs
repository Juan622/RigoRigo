using System.Data;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlCommand = System.Data.SqlClient.SqlCommand;
using RigoRigo.Modelos.Model;

namespace AccesoDatos
{
    public class D_ProductoRigo
    {
        private string connectionString = "Server=localhost;Database=RigoRigoVentasDB;Trusted_Connection=True;";

        public void Producto(ProductoRigoModel productoRigo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    VerProductoSP();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable VerProductoSP()
        {
            System.Data.SqlClient.SqlCommand conect = new SqlCommand("SP_VerProducto");
            conect.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(conect);
            DataTable producto = new DataTable();
            adapter.Fill(producto);
            return producto;
        }
    }
}
