import { Component, OnInit, TemplateRef, Inject, LOCALE_ID  } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Login } from './login';
import { UserService } from 'src/services/user/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit{

  constructor(@Inject(LOCALE_ID) public locale: string,private userService: UserService,
  private modalService: BsModalService) {}


  form: any;
  titleForm: string | undefined;

  fileForm: any;
  
  modalRef: BsModalRef | any;

  ngOnInit(): void {
    this.form = new FormGroup({
      emailAddress: new FormControl(null),
      password: new FormControl(null),
    });
    // this.loginService.GetAll().subscribe((result) => {
    //   this.candidates = result;
    // });
  }

  SendLogin(): void {


    
    
    const login: Login = this.form.value;
      this.userService.Login(login).subscribe((resultado) => {
        console.log(resultado)
        // this.userService.Login().subscribe((registros) => {
        //   this.candidates = registros;
        // });
      });
  }
}

