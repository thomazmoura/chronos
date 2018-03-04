import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Contrato } from '../contrato';
import { ContratoService } from '../contrato.service';

@Component({
  selector: 'app-lista-contrato',
  templateUrl: './lista-contrato.component.html',
  styleUrls: ['./lista-contrato.component.css']
})
export class ListaContratoComponent implements OnInit {

  contratos: Array<Contrato>;
  @Output() contratoSelecionado: EventEmitter<Contrato>;

  constructor(private servico: ContratoService) {
    this.contratoSelecionado = new EventEmitter<Contrato>();
  }

  ngOnInit() {
    this.renovarLista();
  }

  renovarLista() {
    this.servico.get()
      .subscribe(
        contrato => {
          this.contratos = contrato;
          console.log(contrato);
        },
        err => {
          console.log(`Error occured: ${err}`);
        }
      );
  }

  selecionarContrato(contrato: Contrato) {
    this.contratoSelecionado.emit(contrato);
  }

}
