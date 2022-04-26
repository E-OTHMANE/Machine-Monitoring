using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachineController(IMachineService service)
        {
            _machineService = service;
        }

        [HttpGet("/Machines")]
        public async Task<ActionResult<IEnumerable<MachineProductionDTO>>> GetMachines()
        {
            var machines = await _machineService.GetMachines();
            if(((List<MachineProductionDTO>)machines).Count==0)
                return NoContent();
            return Ok(machines);
        }

        [HttpGet("/[controller]/{id}")]
        public async Task<ActionResult<MachineDTO>> GetMachine(int id)
        {
            var machine = await _machineService.GetMachine(id);
            if (machine == null)
                return NotFound();
            return Ok(machine);
        }

        [HttpGet("/[controller]/totalproduction")]
        public async Task<ActionResult> GetTotalProduction([FromQuery] int machineid)
        {
            var machine = await _machineService.GetMachine(machineid);
            if (machine == null)
                return NotFound();
            var totalProduction = await _machineService.GetTotalProduction(machineid);
            var totalProd = new TotalProdcutionClass();
            totalProd.TotalProduction = totalProduction;
            return Ok(totalProd);
        }

        [HttpDelete("/[controller]/{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            var machine = await this._machineService.DeleteMachine(id);
            if (machine == null)
                return NotFound();
            return Ok(machine);
        }

        public class TotalProdcutionClass
        {
            public int TotalProduction { get; set; }
        }
    }
}
