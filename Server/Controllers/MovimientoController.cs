using FinanzasPersonalesProyect.Server.Data;
using FinanzasPersonalesProyect.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanzasPersonalesProyect.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovimientoController(ApplicationDbContext context)
        {

            _context = context;
        }
        [HttpGet]
        [Route("ConexionDB")]
        public async Task<ActionResult<string>> GetConexionDB()
        {
            try
            {
                var consulta = await _context.Movimientos.ToListAsync();
                return "Conectado con base de datos tabla Movimientos";
            }
            catch (Exception ex)
            {
                return "Error. No conectado con la tabla Usuarios";
            }

        }


        [HttpPost("NuevoMovimiento")]
        public async Task<ActionResult<string>> CreateMovimiento(Movimiento objeto)
        {

            _context.Movimientos.Add(objeto);
            await _context.SaveChangesAsync();
            return "Guardado con Exito";
        }

        [HttpGet("ObtenerMovimientos")]
        public async Task<ActionResult<List<Movimiento>>> GetMovimientos()
        {
            var movimientos = await _context.Movimientos.Include(u => u.Usuario).ToListAsync();
            return movimientos;

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<string>> DeleteMovimiento(int id)
        {
            var DbObjeto = await _context.Movimientos.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }
            _context.Movimientos.Remove(DbObjeto);
            await _context.SaveChangesAsync();

            return "Eliminado";
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateMovimiento(Movimiento objeto)
        {

            var DbObjeto = await _context.Movimientos.FindAsync(objeto.Id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.Fecha = objeto.Fecha;
            DbObjeto.Cantidad = objeto.Cantidad;
            DbObjeto.Tipo = objeto.Tipo;
            DbObjeto.Descripcion = objeto.Descripcion;
            DbObjeto.UsuarioId = objeto.UsuarioId;


            await _context.SaveChangesAsync();

            return "Actualizado";

        }


    }
}
