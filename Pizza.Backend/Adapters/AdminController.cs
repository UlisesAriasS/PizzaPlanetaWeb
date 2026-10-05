using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizza.Backend.Domain;
using Pizza.Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Backend.Adapters;

[Authorize(Roles = "Administrador")] // Solo pueden acceder los administradores
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly MainDbContext _context;

    public AdminController(MainDbContext context)
    {
        _context = context;
    }

    [HttpGet("usuarios")]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.Rol)
            .Select(u => new 
            {
                u.Id,
                u.Nombre,
                u.Email,
                u.Telefono,
                Rol = u.Rol != null ? u.Rol.Nombre : "Sin Rol"
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    [HttpPut("usuarios/{usuarioId}/rol/{rolId}")]
    public async Task<IActionResult> AssignRole(int usuarioId, int rolId)
    {
        var user = await _context.Usuarios.FindAsync(usuarioId);
        if (user == null) return NotFound("Usuario no encontrado.");

        var role = await _context.Roles.FindAsync(rolId);
        if (role == null) return NotFound("Rol no encontrado.");

        user.RolId = rolId;
        
        // Registrar en auditoría
        _context.HistorialAccesos.Add(new HistorialAcceso
        {
            UsuarioId = user.Id, // Idealmente el ID del admin que ejecuta esto
            FechaAcceso = System.DateTime.UtcNow,
            Accion = $"Cambio de rol a: {role.Nombre}"
        });

        await _context.SaveChangesAsync();
        return Ok(new { message = "Rol actualizado exitosamente" });
    }

    [HttpGet("auditoria")]
    [Authorize(Policy = "RequireAdmin")] // Ejemplo de cómo se podría usar política más estricta
    public async Task<IActionResult> GetAuditoria()
    {
        var historial = await _context.HistorialAccesos
            .Include(h => h.Usuario)
            .OrderByDescending(h => h.FechaAcceso)
            .Select(h => new 
            {
                h.Id,
                Usuario = h.Usuario.Email,
                h.Accion,
                h.FechaAcceso
            })
            .Take(50)
            .ToListAsync();

        return Ok(historial);
    }
}
