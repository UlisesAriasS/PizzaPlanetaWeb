using System.Collections.Generic;

namespace Pizza.Backend.Domain;

public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
}
