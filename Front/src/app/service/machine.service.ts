import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { MachineProduction } from '../Interfaces/MachineProduction.interface';
import { Machine } from '../Interfaces/Machine.Interface';

@Injectable({
  providedIn: 'root'
})
export class MachineService {
  url = environment.URL;

  constructor(private http:HttpClient) { }

  public getMachineById(id:number):Observable<Machine>{
    return this.http.get<Machine>(this.url+'Machine/'+id);
  }

  public getMachines():Observable<MachineProduction[]>{
    return this.http.get<MachineProduction[]>(this.url+"Machines");
  }

  public getTotalProduction(machineId:number){
    return this.http.get(this.url+"Machine/totalproduction?machineid="+machineId);
  }

  public deleteMachineById(machineId:number):Observable<Machine>{
    return this.http.delete<Machine>(this.url+"Machine/"+machineId)
  }


}
