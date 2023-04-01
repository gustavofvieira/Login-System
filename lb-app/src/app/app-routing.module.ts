import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/user/login/login.component';
import { RecoverPassComponent } from './components/user/recoverPass/recover-pass.component';
import { CreateComponent } from './components/user/create/create.component';
import { UpdatePasswordComponent } from './components/user/updatePassword/update-password.component';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'recoverPass', component: RecoverPassComponent },
  { path: 'create', component: CreateComponent },
  { path: 'updatePassword/:id', component: UpdatePasswordComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
