namespace RigoRigo.Modelos.Model
{
    public class DetallePedidoModel
    {
        public int Id { get; set; }
        public int IdPedidoEncabezado { get; set; }
        public int IdProductosRigo { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorTotal { get; set; }

        // Relación de tablas
        public PedidoEncabezadoModel PedidoEncabezado { get; set; }
        public ProductoRigoModel ProductosRigo { get; set; }
    }
}
