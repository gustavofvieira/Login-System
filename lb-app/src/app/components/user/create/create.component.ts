import { Component, OnInit, TemplateRef, Inject, LOCALE_ID  } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserService } from 'src/services/user/user.service';
import { User } from './user';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})

export class CreateComponent implements OnInit{

  constructor(@Inject(LOCALE_ID) public locale: string,private userService: UserService,
  private modalService: BsModalService) {}


  form: any;
  titleForm: string | undefined;

  fileForm: any;
  
  modalRef: BsModalRef | any;

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl(null),
      emailAddress: new FormControl(null),
      password: new FormControl(null),
      role: new FormControl("Common"),

    });
    // this.loginService.GetAll().subscribe((result) => {
    //   this.candidates = result;
    // });
  }

  Send(): void {
    const user: User = this.form.value;
    console.log(user)
      this.userService.Create(user).subscribe((resultado) => {
        console.log(resultado)
        // this.userService.Login().subscribe((registros) => {
        //   this.candidates = registros;
        // });
      });
  }
}

