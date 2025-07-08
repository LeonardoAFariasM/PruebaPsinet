import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RoverTask } from '../models/rover-task.model';
import { environment } from '../../environments/environment';


@Injectable({ providedIn: 'root' })
export class RoverService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  addTask(roverName: string, task: RoverTask): Observable<RoverTask> {
    return this.http.post<RoverTask>(`${this.apiUrl}/rovers/${roverName}/tasks`, task);
  }

  getDailyTasks(roverName: string, date: Date): Observable<RoverTask[]> {
    const dateStr = date.toISOString().split('T')[0];
    return this.http.get<RoverTask[]>(
      `${this.apiUrl}/rovers/${roverName}/tasks?date=${dateStr}`
    );
  }

  getRovers(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/rovers/names`); // o la ruta que tengas
  }
}