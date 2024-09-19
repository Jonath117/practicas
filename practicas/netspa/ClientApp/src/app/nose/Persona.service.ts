import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PersonaService {
  getData() {
      throw new Error('Method not implemented.');
  }

  private apiUrl = 'http://localhost:5000/nose'; // Cambia la URL por la correcta
  constructor(private http: HttpClient) { }

 

  getEdad(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/MostrarEdad`);
  }

  getDatos(): Observable<{ nombre: string; edad: number }> {
    return this.http.get<{ nombre: string; edad: number }>(`${this.apiUrl}/MostrarDatos`);
  }
}
