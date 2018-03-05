import { Component, OnInit } from '@angular/core';
import { Contrato } from './contrato';

@Component({
  selector: 'app-contrato',
  templateUrl: './contrato.component.html',
  styleUrls: ['./contrato.component.css']
})
export class ContratoComponent implements OnInit {

  contrato: Contrato;
  constructor() { }

  ngOnInit() {
    this.criarNovoContrato();
  }

  criarNovoContrato() {
    this.contrato = new Contrato();
  }

  atualizarContratoDoFormulario(contrato: Contrato) {
    this.contrato = contrato;
  }

}
