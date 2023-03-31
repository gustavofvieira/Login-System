import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { UserService } from 'src/services/user/user.service';
import { LoginComponent } from './components/login/login.component';
import { RecoverPassComponent } from './components/recoverPass/recover-pass.component';
import { CreateComponent } from './components/user/create/create.component';
import { UpdatePasswordComponent } from './components/user/updatePassword/update-password.component';

import { CommonModule } from '@angular/common';
import { HttpClientModule } from "@angular/common/http";
import { ReactiveFormsModule } from "@angular/forms";

import { ModalModule } from "ngx-bootstrap/modal";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RecoverPassComponent,
    CreateComponent,
    UpdatePasswordComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    
  ],
  providers: [HttpClientModule,UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
