import { Component, OnInit, TemplateRef, Inject, LOCALE_ID  } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { UserService } from 'src/services/user/user.service';
import { Login } from '../login/login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-recover-pass',
  templateUrl: './recover-pass.component.html',
  styleUrls: ['./recover-pass.component.css']
})

export class RecoverPassComponent implements OnInit{

  constructor(@Inject(LOCALE_ID) public locale: string,private userService: UserService,
   private router: Router) {}


  form: any;
  titleForm: string | undefined;

  fileForm: any;

  ngOnInit(): void {
    this.form = new FormGroup({
      emailAddress: new FormControl(null),
    });
  }

  SendRecoverPass(): void {
    const login: Login = this.form.value;
      this.userService.RecoverPass(login.emailAddress)
      .subscribe(
        result => {
          alert(result);
          this.Back()
        },
        error => {
          alert(error);
        }
      )
  
  }

  Back(): void {
    this.router.navigate(['/']);
  }
}

