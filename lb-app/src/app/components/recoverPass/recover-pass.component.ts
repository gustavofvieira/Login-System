import { Component, OnInit, TemplateRef, Inject, LOCALE_ID  } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserService } from 'src/services/user/user.service';
import { Login } from '../login/login';

@Component({
  selector: 'app-recover-pass',
  templateUrl: './recover-pass.component.html',
  styleUrls: ['./recover-pass.component.css']
})

export class RecoverPassComponent implements OnInit{

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

  SendRecoverPass(): void {
    const login: Login = this.form.value;
      this.userService.RecoverPass(login.emailAddress).subscribe((resultado) => {
        console.log(resultado)
        // this.userService.Login().subscribe((registros) => {
        //   this.candidates = registros;
        // });
      });
  }
}

