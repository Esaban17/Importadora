using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {

        // GET: api/<CompraController>
        [HttpGet]
        [Route("GetCompras")]
        public async Task<IEnumerable<ImportadoraModels.Compra>> GetCompras()
        {
            ImportadoraContext _importadoraContext = new ImportadoraContext();
            IEnumerable<ImportadoraModels.Compra> Compras = _importadoraContext.Compras.Select(s =>
            new ImportadoraModels.Compra
            {
                Id = s.Id,
                UsuarioId = s.UsuarioId,
                VehiculoId = s.VehiculoId,
                FechaCompra = s.FechaCompra,
                Cantidad = s.Cantidad,
                PrecioTotal = s.PrecioTotal
            }
            ).ToList();
            return Compras;
        }

        // GET api/<CompraController>/5
        [Route("GetCompra")]
        [HttpGet]
        public async Task<ImportadoraModels.Compra> GetCompra(int id)
        {
            ImportadoraContext _importadoraContext = new ImportadoraContext();
            ImportadoraModels.Compra Compra = await _importadoraContext.Compras.Select(s =>
            new ImportadoraModels.Compra
            {
                Id = s.Id,
                UsuarioId = s.UsuarioId,
                VehiculoId = s.VehiculoId,
                FechaCompra = s.FechaCompra,
                Cantidad = s.Cantidad,
                PrecioTotal = s.PrecioTotal
            }
            ).FirstAsync(s => s.Id == id);
            return Compra;
        }

        // POST api/<CompraController>
        [Route("Create")]
        [HttpPost]
        public async Task<ImportadoraModels.GeneralResult> Create(ImportadoraModels.Compra Compra)
        {
            ImportadoraModels.GeneralResult generalResult = new ImportadoraModels.GeneralResult()
            {
                Result = false
            };

            try
            {
                ImportadoraContext _importadoraContext = new ImportadoraContext();
                Models.Compra newCompra = new Models.Compra
                {
                    UsuarioId = Compra.UsuarioId,
                    VehiculoId = Compra.VehiculoId,
                    FechaCompra = Compra.FechaCompra,
                    Cantidad = Compra.Cantidad,
                };
                _importadoraContext.Compras.Add(newCompra);
                await _importadoraContext.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }

        // PUT api/<CompraController>/5
        [Route("Update")]
        [HttpPut]
        public async Task<ImportadoraModels.GeneralResult> Update(ImportadoraModels.Compra Compra)
        {
            ImportadoraModels.GeneralResult generalResult = new ImportadoraModels.GeneralResult()
            {
                Result = false
            };

            try
            {
                ImportadoraContext _importadoraContext = new ImportadoraContext();
                var CompraToUpdate = await _importadoraContext.Compras.FindAsync(Compra.Id);
                if (CompraToUpdate != null)
                {
                    CompraToUpdate.UsuarioId = Compra.UsuarioId;
                    CompraToUpdate.VehiculoId = Compra.VehiculoId;
                    CompraToUpdate.FechaCompra = Compra.FechaCompra;
                    CompraToUpdate.Cantidad = Compra.Cantidad;

                    await _importadoraContext.SaveChangesAsync();
                    generalResult.Result = true;
                }
                else
                {
                    generalResult.ErrorMessage = $"Compra con id={Compra.Id} no encontrado.";
                }
            }
            catch (Exception ex)
            {
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }

        // DELETE api/<CompraController>/5
        [Route("Delete")]
        [HttpDelete]
        public async Task<ImportadoraModels.GeneralResult> Delete(int id)
        {
            ImportadoraModels.GeneralResult generalResult = new ImportadoraModels.GeneralResult()
            {
                Result = false
            };

            using (var dbContext = new ImportadoraContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var Compra = await dbContext.Compras.FindAsync(id);

                        if (Compra != null)
                        {

                            dbContext.Compras.Remove(Compra);

                            dbContext.SaveChanges();

                            transaction.Commit();
                        }
                        else
                        {
                            generalResult.ErrorMessage = $"Compra con id={id} no encontrado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return generalResult;
        }
    }
}
