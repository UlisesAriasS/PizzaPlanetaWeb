using System;

namespace Pizza.Backend.Domain;

public class HistorialAcceso
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; } = null!;
    
    public DateTime FechaAcceso { get; set; }
    public string Accion { get; set; } = null!;
    public string? DireccionIp { get; set; }
}
