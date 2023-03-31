import { Component, OnInit, TemplateRef, Inject, LOCALE_ID  } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserService } from 'src/services/user/user.service';
import { Login } from '../../login/login';

@Component({
  selector: 'app-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.css']
})

export class UpdatePasswordComponent implements OnInit{

  constructor(@Inject(LOCALE_ID) public locale: string,private userService: UserService,
  private modalService: BsModalService) {}


  form: any;
  titleForm: string | undefined;

  fileForm: any;
  
  modalRef: BsModalRef | any;

  ngOnInit(): void {
    this.form = new FormGroup({
      emailAddress: new FormControl(null),
    });
    // this.loginService.GetAll().subscribe((result) => {
    //   this.candidates = result;
    // });
  }

  Send(): void {
    const login: Login = this.form.value;
    console.log("Login: ",login)
      this.userService.RecoverPass(login.password).subscribe((resultado) => {
        console.log(resultado)
        // this.userService.Login().subscribe((registros) => {
        //   this.candidates = registros;
        // });
      });
  }
}

