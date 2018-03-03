import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Contrato } from '../contrato';
import { ContratoService } from '../contrato.service';

@Component({
  selector: 'app-lista-contrato',
  templateUrl: './lista-contrato.component.html',
  styleUrls: ['./lista-contrato.component.css']
})
export class ListaContratoComponent implements OnInit {

  contratos: Array<Contrato> = [];
  constructor(private servico: ContratoService) { }

  ngOnInit() {
    this.servico.get()
      .subscribe(contratos => this.contratos.push(contratos));
  }

}
