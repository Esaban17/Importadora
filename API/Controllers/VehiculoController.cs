using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehiculoController : ControllerBase
    {
        // GET: api/<VehiculoController>
        [HttpGet]
        [Route("GetVehiculos")]
        public async Task<IEnumerable<ImportadoraModels.Vehiculo>> GetVehiculos()
        {
            ImportadoraContext _importadoraContext = new ImportadoraContext();
            IEnumerable<ImportadoraModels.Vehiculo> Vehiculos = _importadoraContext.Vehiculos.Select(s =>
            new ImportadoraModels.Vehiculo
            {
                Id = s.Id,
                Marca = s.Marca,
                Modelo = s.Modelo,
                Anio = s.Anio,
                Precio = s.Precio,
                Cantidad = s.Cantidad
            }
            ).ToList();
            return Vehiculos;
        }

        // GET api/<VehiculoController>/5
        [Route("GetVehiculo")]
        [HttpGet]
        public async Task<ImportadoraModels.Vehiculo> GetVehiculo(int id)
        {
            ImportadoraContext _importadoraContext = new ImportadoraContext();
            ImportadoraModels.Vehiculo Vehiculo = await _importadoraContext.Vehiculos.Select(s =>
            new ImportadoraModels.Vehiculo
            {
                Id = s.Id,
                Marca = s.Marca,
                Modelo = s.Modelo,
                Anio = s.Anio,
                Precio = s.Precio,
                Cantidad = s.Cantidad
            }
            ).FirstAsync(s => s.Id == id);
            return Vehiculo;
        }

        // POST api/<VehiculoController>
        [Route("Create")]
        [HttpPost]
        public async Task<ImportadoraModels.GeneralResult> Create(ImportadoraModels.Vehiculo Vehiculo)
        {
            ImportadoraModels.GeneralResult generalResult = new ImportadoraModels.GeneralResult()
            {
                Result = false
            };

            try
            {
                ImportadoraContext _importadoraContext = new ImportadoraContext();
                Models.Vehiculo newVehiculo = new Models.Vehiculo
                {
                    Marca = Vehiculo.Marca,
                    Modelo = Vehiculo.Modelo,
                    Anio = Vehiculo.Anio,
                    Precio = Vehiculo.Precio,
                    Cantidad = Vehiculo.Cantidad
                };
                _importadoraContext.Vehiculos.Add(newVehiculo);
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

        // PUT api/<VehiculoController>/5
        [Route("Update")]
        [HttpPut]
        public async Task<ImportadoraModels.GeneralResult> Update(ImportadoraModels.Vehiculo Vehiculo)
        {
            ImportadoraModels.GeneralResult generalResult = new ImportadoraModels.GeneralResult()
            {
                Result = false
            };

            try
            {
                ImportadoraContext _importadoraContext = new ImportadoraContext();
                var VehiculoToUpdate = await _importadoraContext.Vehiculos.FindAsync(Vehiculo.Id);
                if (VehiculoToUpdate != null)
                {
                    VehiculoToUpdate.Marca = Vehiculo.Marca;
                    VehiculoToUpdate.Modelo = Vehiculo.Modelo;
                    VehiculoToUpdate.Anio = Vehiculo.Anio;
                    VehiculoToUpdate.Precio = Vehiculo.Precio;
                    VehiculoToUpdate.Cantidad = Vehiculo.Cantidad;

                    await _importadoraContext.SaveChangesAsync();
                    generalResult.Result = true;
                }
                else
                {
                    generalResult.ErrorMessage = $"Vehiculo con id={Vehiculo.Id} no encontrado.";
                }
            }
            catch (Exception ex)
            {
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }

        // DELETE api/<VehiculoController>/5
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
                        var Vehiculo = await dbContext.Vehiculos.FindAsync(id);

                        if (Vehiculo != null)
                        {

                            dbContext.Vehiculos.Remove(Vehiculo);

                            dbContext.SaveChanges();

                            transaction.Commit();
                        }
                        else
                        {
                            generalResult.ErrorMessage = $"Vehiculo con id={id} no encontrado.";
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
