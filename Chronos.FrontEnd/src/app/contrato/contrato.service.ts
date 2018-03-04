import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.prod';
import { Observable } from 'rxjs/Observable';
import { Contrato } from './contrato';

@Injectable()
export class ContratoService {
  baseUrl = `${environment.apiBaseUrl}contratos`;

  constructor(private http: HttpClient) { }

  get(): Observable<Array<Contrato>> {
    return this.http.get<Contrato[]>(this.baseUrl);
  }

  post(contrato: Contrato) {
    this.http.post<Contrato>(this.baseUrl, contrato)
      .subscribe(
        res => {
          console.log(res);
        },
        err => {
          console.log('Error occured');
        }
      );
  }

  put(contrato: Contrato) {
    this.http.post<Contrato>(this.baseUrl, contrato)
      .subscribe(
        res => {
          console.log(res);
        },
        err => {
          console.log('Error occured');
        }
      );
  }

  delete(id: string) {
    this.http.delete(this.baseUrl, {
      params: {
        id: id
      }
    });
  }
}
