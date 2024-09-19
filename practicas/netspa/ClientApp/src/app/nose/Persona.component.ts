import { Component, OnInit } from '@angular/core';
import { PersonaService } from "./Persona.service";

@Component({
  selector: 'app-Persona',
  templateUrl: './Persona.html',
  styleUrls: ['./Persona.css']
})
export class PersonaComponent implements OnInit {
  public nombre: string = "Jonathan";
  public edad: number = 19;

  constructor(private personaService: PersonaService) {}

  ngOnInit(): void {
    this.getData();
    };

    getData(){
        this.personaService.getDatos().subscribe((result: { nombre: string; edad: number; }) => {
            this.nombre = result.nombre;
            this.edad = result.edad;
        }, (error: string) => {
            console.error('error obteniendo datos', error);
        })
    }
  
}