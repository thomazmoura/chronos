import { Component, OnInit, Input } from '@angular/core';

import { Contrato } from '../contrato';
import { HttpClient } from '@angular/common/http';
import { ContratoService } from '../contrato.service';

@Component({
  selector: 'app-formulario-contrato',
  templateUrl: './formulario-contrato.component.html',
  styleUrls: ['./formulario-contrato.component.css']
})
export class FormularioContratoComponent implements OnInit {

  @Input() public contrato: Contrato;

  constructor(private servico: ContratoService) {
    if (!this.contrato) {
      this.contrato = new Contrato();
    }
  }

  ngOnInit() {
  }

  salvar() {
    this.servico.post(this.contrato);
  }

}
