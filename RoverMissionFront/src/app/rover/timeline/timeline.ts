import { Component, inject, OnChanges, Input, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoverTask } from '../../models/rover-task.model';
import { RoverService } from '../../services/rover.service';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef } from '@angular/core';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-timeline',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './timeline.html',
  styleUrls: ['./timeline.css']
})

export class Timeline implements OnChanges {
  @Input() roverName: string = 'All';
  tasks: RoverTask[] = [];

  constructor(private http: HttpClient) {}
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['roverName'] && changes['roverName'].currentValue) {
      this.loadTasks();
    }
  }
  ngOnInit():void {
    this.loadTasks();

  }
  
  loadTasks() {
    const date = '2025-07-07';

    if(this.roverName == 'All') {
      this.http.get<RoverTask[]>(`http://localhost:5286/rovers/tasks?date=${date}`).subscribe({
        next: (tasks) => {
          this.tasks = tasks;
          console.log('Tareas de todos los rovers:', this.tasks);
        },
        error: (err) => {
          console.error("Error loading all tasks:", err);
        }
      });
    } else {
      this.http.get<RoverTask[]>(`http://localhost:5286/rovers/${this.roverName}/tasks?date=${date}`).subscribe({
      next: (task) => {
        this.tasks = task;
        console.log('Tareas recibidas:', this.tasks);

      },
      error: (err) => {
        console.error("Error loading tasks:", err);
      }
    });
    }
    
  }

  getEndTime(task: RoverTask): Date {
    const start = new Date(task.startsAt);
    return new Date(start.getTime() + task.durationMinutes * 60 * 1000);
  }

  calculatePosition(task: RoverTask): number {
    const start = new Date(task.startsAt);
    const minutes = start.getHours() * 60 + start.getMinutes();
    return (minutes / 1440) * 100;
  }

  calculateWidth(task: RoverTask): number {
    return (task.durationMinutes / 1440) * 100;
  }

  getTaskClass(task: RoverTask): string {
    switch (task.taskType) {
      case 'Drill': return 'task-drill';
      case 'Sample': return 'task-sample';
      case 'Photo': return 'task-photo';
      case 'Charge': return 'task-charge';
      default: return '';
    }
  }

}