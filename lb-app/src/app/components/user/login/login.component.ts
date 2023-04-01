import { Component, OnInit, TemplateRef, Inject, LOCALE_ID  } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Login } from './login';
import { UserService } from 'src/services/user/user.service';
import { LocalStorageService } from 'src/services/local-storage.service';
import { Token } from './token';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit{

  constructor(@Inject(LOCALE_ID) public locale: string,private userService: UserService,
  private router: Router,private localStorageService: LocalStorageService) {}


  form: any;
  titleForm: string | undefined;
  token!: Token;
  fileForm: any;
  
  modalRef: BsModalRef | any;

  ngOnInit(): void {
    this.form = new FormGroup({
      emailAddress: new FormControl(null),
      password: new FormControl(null),
    });
  }

  Send(): void {

    const login: Login = this.form.value;
      this.userService.Login(login).subscribe((result) => {
        this.token = result;
        this.localStorageService.set("token", this.token.jwtKey);
        this.router.navigate(['/home']);
      });
  }
}

