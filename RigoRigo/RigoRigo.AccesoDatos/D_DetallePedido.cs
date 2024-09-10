using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using RigoRigo.Modelos.Model;

namespace AccesoDatos
{
    public class D_DetallePedido
    {
        public string connectionString = "Server=localhost;Database=RigoRigoVentasDB;Trusted_Connection=True;";

        public void DetallePedido(DetallePedidoModel pedido)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var pedidoJson = JsonConvert.SerializeObject(pedido);
           
                    InsertarPedido(pedidoJson);  
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertarPedido(string pedidoJson)
        {
            using (SqlCommand command = new SqlCommand("SP_InsertarPedido"))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pedidoJson", pedidoJson);
                command.ExecuteNonQuery();
            }
        }
    }
}
