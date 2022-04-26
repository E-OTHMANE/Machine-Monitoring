using AutoMapper;
using AutoMapper.QueryableExtensions;
using Models;
using Repository;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MachineService : IMachineService
    {
        private IMachineRepository _repository;
        private IMapper _mapper;

        public MachineService(IMachineRepository repository,IMapper mapper )
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<MachineDTO> DeleteMachine(int machineId)
        {
            var result= await this._repository.DeleteMachineById(machineId);
            return _mapper.Map<Machine, MachineDTO>(result);
        }

        public async Task<MachineDTO> GetMachine(int machineId)
        {
            var result=await this._repository.GetMachineById(machineId);
           return _mapper.Map<Machine,MachineDTO>(result);
        }

        public async Task<IEnumerable<MachineProductionDTO>> GetMachines()
        {
            var result = await Task.FromResult(this._repository.GetMachines());
            return _mapper.Map< IEnumerable<Machine> ,IEnumerable <MachineProductionDTO>>(result);
        }

        public async Task<int> GetTotalProduction(int machineId)
        {
            return await Task.FromResult(this._repository.GetTotalProductionByMachineId(machineId));
        }
    }
}
