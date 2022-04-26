using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MachineRepository : IMachineRepository
    {
        private readonly MachineMonitoringContext _context;

        public MachineRepository(MachineMonitoringContext context)
        {
            _context = context;
        }


        public async Task<Machine> DeleteMachineById(int machineId)
        {
            Machine machine = await _context.Machines.FindAsync(machineId);
            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();
            return machine;
        }

        public async Task<Machine> GetMachineById(int machineId)
        {
            return await _context.Machines.FindAsync(machineId);
        }

        public IEnumerable<Machine> GetMachines()
        {
            return _context.Machines.Include(element => element.MachineProductions).AsEnumerable();
        }

        public int GetTotalProductionByMachineId(int machineId)
        {
            return _context.MachineProductions.Where(machine => machine.MachineId == machineId)
                                              .Sum(element => element.TotalProduction);
        }
    }
}
