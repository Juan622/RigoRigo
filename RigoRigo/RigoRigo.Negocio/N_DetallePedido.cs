using RigoRigo.Modelos.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    internal class N_DetallePedido
    {
            public DetallePedidoModel CrearDetallePedido(int idPedidoEncabezado, int idProductosRigo, int cantidad, decimal valorTotal)
            {
                var detallePedido = new DetallePedidoModel
                {
                    IdPedidoEncabezado = idPedidoEncabezado,
                    IdProductosRigo = idProductosRigo,
                    Cantidad = cantidad,
                    ValorTotal = valorTotal
                };

                detallePedido.PedidoEncabezado = ObtenerPedidoEncabezadoPorId(idPedidoEncabezado);
                detallePedido.ProductosRigo = ObtenerProductoRigoPorId(idProductosRigo);

                return detallePedido;
            }

            private PedidoEncabezadoModel ObtenerPedidoEncabezadoPorId(int idPedidoEncabezado)
            {
                return new PedidoEncabezadoModel
                {
                    Id = idPedidoEncabezado,
                    NumeroIdentificacion = "",
                    DireccionEntrega = ""
                };
            }

            private ProductoRigoModel ObtenerProductoRigoPorId(int idProductosRigo)
            {
                return new ProductoRigoModel
                {
                    Id = idProductosRigo,
                    Articulo = "",
                    ValorUnitario = 0,
                    CantidadExistente = 0
                };
            }
    }
}
