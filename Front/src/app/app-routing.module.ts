import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MachineComponent } from './machine/machine.component';
import { MachineDetailsComponent } from './machine/machine-details/machine-details.component';



const routes: Routes = [
  { path: '', component: MachineComponent },
  { path: 'Dashboard/:id', component: MachineDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
