using System;
using Microsoft.AspNetCore.Mvc;
using CRUD.Services;
using CRUD.Models;

namespace CRUD.Controllers;

[Controller]
[Route("api/[controller]")]
public class CarroController: Controller {
    
    private readonly MongoDBService _mongoDBService;

    public CarroController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Carro>> Get() {
        return await _mongoDBService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Carro carro) {
        if (carro == null){
        return BadRequest("Carro não pode ser nulo.");
        }

        await _mongoDBService.CreateAsync(carro);
        return CreatedAtAction(nameof(Get), new { id = carro.Id}, carro);
    }

    public class CarroUpdateModel {
        public string Modelo { get; set; } = null!;
        public string Plate { get; set; } = null!;
        public int YearManufacture { get; set; } = 0;
        public List<string> Accessory { get; set; } = new List<string>();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCarro(string id, [FromBody] CarroUpdateModel carroUpdateModel) {
        if (carroUpdateModel == null) {
            return BadRequest(new { message = "Dados inválidos" });
        }

        try {
            await _mongoDBService.AddToCarroAsync(
                id,
                carroUpdateModel.Modelo,
                carroUpdateModel.Plate,
                carroUpdateModel.YearManufacture,
                carroUpdateModel.Accessory 
            );
            return Ok(new { message = "Carro atualizado com sucesso", id = id, carroUpdateModel.Modelo, carroUpdateModel.Plate, carroUpdateModel.YearManufacture, carroUpdateModel.Accessory });
        } catch (Exception ex) {
            return StatusCode(500, new { message = "Erro ao atualizar carro", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _mongoDBService.DeleteAsync(id);
        return Ok(new {message = "Carro deletado com sucesso"});;
    }


}