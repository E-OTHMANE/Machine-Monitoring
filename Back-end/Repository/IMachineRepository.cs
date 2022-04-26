using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IMachineRepository
    {

        public IEnumerable<Machine> GetMachines();

        public Task<Machine> GetMachineById(int machineId);

        public Task<Machine> DeleteMachineById(int machineId);

        public int GetTotalProductionByMachineId(int machineId);
    }
}
