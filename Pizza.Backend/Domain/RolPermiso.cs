namespace Pizza.Backend.Domain;

public class RolPermiso
{
    public int RolId { get; set; }
    public virtual Rol Rol { get; set; } = null!;

    public int PermisoId { get; set; }
    public virtual Permiso Permiso { get; set; } = null!;
}
