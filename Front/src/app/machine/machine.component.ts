import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Machine } from '../Interfaces/Machine.Interface';
import { MachineProduction } from '../Interfaces/MachineProduction.interface';
import { MachineService } from '../service/machine.service';

@Component({
  selector: 'app-machine',
  templateUrl: './machine.component.html',
  styleUrls: ['./machine.component.css']
})
export class MachineComponent implements OnInit {

  machines: MachineProduction[] = [];
  selectedMachine!: Machine;
  deletedMachine?: Machine;
  hasError: boolean = false;


  constructor(private machineService: MachineService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getMachines();
  }

  getMachines() {
    this.machineService.getMachines().subscribe((data: MachineProduction[]) => {

    });

    this.machineService.getMachines().subscribe({
      next: (data: MachineProduction[]) => data == null ? this.machines = [] : this.machines = data,
      error: (err: Error) => this.hasError = true,
      complete: () => this.hasError = false
    })
  }

  deleteMachine(machineId: number) {
    this.machines = this.machines?.filter(function (element: MachineProduction) {
      return element.machineId != machineId;
    });
    this.machineService.deleteMachineById(machineId).subscribe(data => {
      console.log(data);
    });
  }

}
