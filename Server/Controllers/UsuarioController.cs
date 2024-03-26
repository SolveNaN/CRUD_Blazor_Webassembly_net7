using FinanzasPersonalesProyect.Server.Data;
using FinanzasPersonalesProyect.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace FinanzasPersonalesProyect.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {

            _context = context;
        }



        [HttpGet("ConexionServidor")]
        public async Task<ActionResult<string>> GetEjemplo()
        {
            return "Conectado con el servidor";
        }


        [HttpGet]
        [Route("ConexionDB")]
        public async Task<ActionResult<string>> GetConexionDB()
        {
            try
            {
                var consulta = await _context.Usuarios.ToListAsync();
                return "Conectado con base de datos tabla Usuarios";
            }
            catch (Exception ex)
            {
                return "Error. No conectado con la tabla Usuarios";
            }
                        
        }

        [HttpPost("NuevoUsuario")]
        public async Task<ActionResult<string>> CreateUsuario(Usuario objeto)
        {

            _context.Usuarios.Add(objeto);
            await _context.SaveChangesAsync();
            return "Guardado con Exito";
        }

        [HttpGet("ObtenerUsuarios")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios;

        }



    }
}
