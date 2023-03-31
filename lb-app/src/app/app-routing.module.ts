import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RecoverPassComponent } from './components/recoverPass/recover-pass.component';
import { CreateComponent } from './components/user/create/create.component';
import { UpdatePasswordComponent } from './components/user/updatePassword/update-password.component';

// const routes: Routes = [];
const routes: Routes = [
  // { path: '', component: HomeComponent },
  { path: '', component: LoginComponent },
  { path: 'recoverPass', component: RecoverPassComponent },
  { path: 'create', component: CreateComponent },
  { path: 'updatePassword', component: UpdatePasswordComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
