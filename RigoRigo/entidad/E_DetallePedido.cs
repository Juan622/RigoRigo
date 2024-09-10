namespace AccesoDatos
{
    public class E_DetallePedido
    {
        public int Id { get; set; }
        public int IdPedidoEncabezado { get; set; }
        public int IdProductosRigo { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorTotal { get; set; }

        // Relación de tablas
        public E_PedidoEncabezado PedidoEncabezado { get; set; }
        public E_ProductoRigo ProductosRigo { get; set; }
    }
}
