using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Service;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;
using static WebAPI.Controllers.MachineController;

namespace WebAPITest
{
    public class UnitTestMachineController
    {
        [Fact]
        public void GetMachines_ItemsExist()
        {
            var mokeService = new Mock<IMachineService>();
            mokeService.Setup(_ => _.GetMachines()).Returns(this.MokGetMachines());

            var controller = new MachineController(mokeService.Object);
            var machines = controller.GetMachines().Result;

            var result = (OkObjectResult) machines.Result;
            var collection = (IEnumerable<MachineProductionDTO>) result.Value;


            Assert.IsType<OkObjectResult>(result);
            Assert.NotEmpty(collection);
        }
        
        [Fact]
        public void GetMachines_ItemsNotExist()
        {
            var emptyList = Task.FromResult((IEnumerable<MachineProductionDTO>)new List<MachineProductionDTO>());
            var mokeService = new Mock<IMachineService>();
            mokeService.Setup(_ => _.GetMachines()).Returns(emptyList);

            var controller = new MachineController(mokeService.Object);
            var machines = controller.GetMachines();

            var result = (NoContentResult)machines.Result.Result;
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetMachine_ItemExist()
        {
            var machineTest = new MachineDTO()
            {
                MachineId = 1,
                Name = "Machine 1",
                Description = "Description of machine 1"
            };

            var mokeService = new Mock<IMachineService>();
            mokeService.Setup(_ => _.GetMachine(1)).Returns(Task.FromResult(machineTest));

            var controller = new MachineController(mokeService.Object);
            var machine = controller.GetMachine(1).Result;

            var result = (OkObjectResult)machine.Result;
            var myMachineResult = (MachineDTO)result.Value;


            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(machineTest,myMachineResult);
        }

        [Fact]
        public void GetMachine_ItemNotExist()
        {
            int id = new Random().Next();
            var mokeService = new Mock<IMachineService>();
            mokeService.Setup(_ => _.GetMachine(id)).Returns(Task.FromResult((MachineDTO)null));

            var controller = new MachineController(mokeService.Object);
            var machine = controller.GetMachine(id);

            var result = (NotFoundResult)machine.Result.Result;
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetTotalProduction_ItemExist()
        {
            var totalProduction = new TotalProdcutionClass();
            totalProduction.TotalProduction = this.MokGetMachinesProduction();
            var machine = new MachineDTO() { MachineId = 1 };
            var mokeService = new Mock<IMachineService>();
            mokeService.Setup(_ => _.GetMachine(1)).ReturnsAsync(machine);
            mokeService.Setup(_ => _.GetTotalProduction(1)).ReturnsAsync((int)totalProduction.TotalProduction);

            var controller = new MachineController(mokeService.Object);
            var _totalProduction = controller.GetTotalProduction(1);
            var result =(OkObjectResult) _totalProduction.Result;
            var value = result.Value;

            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<TotalProdcutionClass>(value);
        }

        [Fact]
        public void GetTotalProduction_ItemNotExist()
        {
            var totalProducion = new List<MachineProduction>().Sum(element => element.TotalProduction);
            var mokeService = new Mock<IMachineService>();
            mokeService.Setup(_ => _.GetTotalProduction(1)).Returns(Task.FromResult(totalProducion));

            var controller = new MachineController(mokeService.Object);
            var machineTotalProduction = controller.GetTotalProduction(1).Result;

            var result = (NotFoundResult)machineTotalProduction;


            Assert.IsType<NotFoundResult>(result);
        }

        public Task<IEnumerable<MachineProductionDTO>> MokGetMachines()
        {
            List<MachineProductionDTO> machines = new List<MachineProductionDTO>();
            for (int i = 1; i < 6; i++)
            {
                MachineProductionDTO machine = new MachineProductionDTO();
                machine.MachineId = i;
                machine.Name = "Equipement " + i;
                machine.TotalProduction = new Random().Next(4, 12);
                machines.Add(machine);
            }
            return Task.FromResult((IEnumerable<MachineProductionDTO>)machines);
        }


        public int MokGetMachinesProduction()
        {
            List<MachineProduction> machines = new List<MachineProduction>();
            for (int i = 1; i < 6; i++)
            {
                MachineProduction machineProd = new MachineProduction();
                machineProd.MachineProductionId = i;
                machineProd.MachineId = 1;
                machineProd.TotalProduction = 5;
                machines.Add(machineProd);
            }
            int Sum = machines.Where(x=> x.MachineId == 1).Sum(e=>e.TotalProduction);
            return Sum;
        }


    }
}
