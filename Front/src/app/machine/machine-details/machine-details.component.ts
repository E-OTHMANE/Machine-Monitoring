import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { timer } from 'rxjs';
import { Machine } from 'src/app/Interfaces/Machine.Interface';
import { MachineService } from 'src/app/service/machine.service';

@Component({
  selector: 'app-machine-details',
  templateUrl: './machine-details.component.html',
  styleUrls: ['./machine-details.component.css']
})
export class MachineDetailsComponent implements OnInit {

  selectedMachine!:Machine
  totalProdcution?:any;
  reloadTimer=timer(2,5000);
  errHandler?:any;

  constructor(private machineService:MachineService,private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.reloadTimer.subscribe(data=>{
    let machineId = this.route.snapshot.params?.['id'];
    this.getTotalProductions(machineId);
    this.getMachine(machineId);
    });
  }

  getTotalProductions(machineId:number)
  {
    this.machineService.getTotalProduction(machineId).subscribe({
      next :(data:any)=> this.totalProdcution=data?.['totalProduction'],
      error :(err:Error) => this.errHandler ="Machine that have id = "+machineId+" doesn't exist in database" 
    });
  }
  getMachine(machineId: number) {
    this.machineService.getMachineById(machineId).subscribe((data: Machine) => {
      this.selectedMachine = data;
    });
  }

}