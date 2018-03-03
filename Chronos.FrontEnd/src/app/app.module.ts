import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { ContratoComponent } from './contrato/contrato.component';
import { ListaContratoComponent } from './contrato/lista-contrato/lista-contrato.component';
import { FormularioContratoComponent } from './contrato/formulario-contrato/formulario-contrato.component';
import { ContratoService } from './contrato/contrato.service';


@NgModule({
  declarations: [
    AppComponent,
    ContratoComponent,
    ListaContratoComponent,
    FormularioContratoComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [ContratoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
