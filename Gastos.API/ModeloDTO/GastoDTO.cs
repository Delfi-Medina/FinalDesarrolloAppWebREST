namespace Gastos.API.Modelo.DTO
{
    public class GastoDTO
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public int Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
