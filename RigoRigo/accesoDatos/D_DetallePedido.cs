using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AccesoDatos
{
    public class D_DetallePedido
    {
        private string connectionString = "Server=localhost;Database=RigoRigoVentasDB;Trusted_Connection=True;";

        public void DetallePedido(E_DetallePedido pedido)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Desserializa el Modelo Pedido a JSON
                    var pedidoJson = JsonConvert.SerializeObject(pedido);

                    // Invocar el procedimiento almacenado
                    InsertarPedido(pedidoJson);  
                }
            }
            catch (Exception ex)
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
