using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IMachineService
    {

        public Task<MachineDTO> DeleteMachine(int machineId);

        public Task<int> GetTotalProduction(int machineId);

        public Task<IEnumerable<MachineProductionDTO>> GetMachines();

        public Task<MachineDTO> GetMachine(int machineId);

    }
}
