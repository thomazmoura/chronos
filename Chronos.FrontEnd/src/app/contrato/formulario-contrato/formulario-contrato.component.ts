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
  @Output() public contratoResetado: EventEmitter<void>;

  constructor(private servico: ContratoService) {
    this.contratoResetado = new EventEmitter();
  }

  ngOnInit() {
  }

  salvar() {
    if (this.contrato.id) {
      this.servico.put(this.contrato);
    } else {
      this.servico.post(this.contrato);
    }
  }

  excluir() {
    this.servico.delete(this.contrato.id);
  }

  criarNovoContrato() {
    this.contratoResetado.emit();
  }

}
