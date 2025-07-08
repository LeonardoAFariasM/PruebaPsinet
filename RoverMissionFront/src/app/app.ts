import { Component, OnInit, ChangeDetectorRef  } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Timeline } from './rover/timeline/timeline';
import {FormsModule} from '@angular/forms';
import { RoverService } from './services/rover.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Timeline, FormsModule, CommonModule ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  selectedRover = 'All';
  rovers: string[] = [];

  constructor(private roverService: RoverService, private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    this.roverService.getRovers().subscribe({
      next: (data) => {
        this.rovers = data;

        this.selectedRover = 'All';
      },
      error: (err) => console.error('Error fetching rovers:', err)
    });
  }

  onRoverChanged(event: Event) {
    const selected = (event.target as HTMLSelectElement).value;
    this.selectedRover = selected;
    console.log('Rover seleccionado:', this.selectedRover);
  }

}
