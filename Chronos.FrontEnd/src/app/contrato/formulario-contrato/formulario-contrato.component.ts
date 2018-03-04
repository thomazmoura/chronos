import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

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
  @Output() public listaDeContratosAlterada: EventEmitter<void>;

  constructor(private servico: ContratoService) {
    this.listaDeContratosAlterada = new EventEmitter();
  }

  ngOnInit() {
    if (!this.contrato) {
      this.criarNovoContrato();
    }
  }

  salvar() {
    this.servico.post(this.contrato);
  }

  excluir() {
    this.servico.delete(this.contrato.id);
    this.listaDeContratosAlterada.emit();
  }

  criarNovoContrato() {
    this.contrato = new Contrato();
  }

}
