import { Component, OnInit, TemplateRef, Inject, LOCALE_ID  } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserService } from 'src/services/user/user.service';
import { Login } from '../login/login';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.css']
})

export class UpdatePasswordComponent implements OnInit{

  constructor(@Inject(LOCALE_ID) public locale: string,private userService: UserService,
  private modalService: BsModalService, private route: ActivatedRoute,) {}


  form: any;
  titleForm: string | undefined;
  id: any;
  fileForm: any;
  
  modalRef: BsModalRef | any;

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.id = params['id'];
    });
    this.form = new FormGroup({
      password: new FormControl(null),
    });
  }

  Send(): void {
    const login: Login = this.form.value;
    console.log("Login: ",login)
      this.userService.UpdatePassword(login.password).subscribe((resultado) => {
        console.log(resultado)
        // this.userService.Login().subscribe((registros) => {
        //   this.candidates = registros;
        // });
      });
  }
}

