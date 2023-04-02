import { Component, OnInit, Inject, LOCALE_ID  } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import { UserService } from 'src/services/user/user.service';
import { User } from './user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})

export class CreateComponent implements OnInit{

  constructor(@Inject(LOCALE_ID) public locale: string,private userService: UserService, private router: Router) {}


  form: any;
  titleForm: string | undefined;
  fileForm: any;
  confirmPassword!: string;
  passwordStrength!: number;

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl(null),
      emailAddress: new FormControl(null),
      password: new FormControl(null),
      confirmPassword: new FormControl(null),
      role: new FormControl("Common"),

    });
  }

  Send(): void {
    const user: User = this.form.value;
    if(user.confirmPassword !== user.password){
      alert("The passwords must be same!!!")
      return;
    }
    console.log(user)
    if (user.emailAddress && !/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/.test(user.emailAddress)) {
      alert("Email Address not valid")
      return;
    }
      this.userService.Create(user).subscribe(
        result => {
          alert(result);
          this.Back()
        },
        error => {
          alert(error);
        }
      );
  }

  Back(): void {
    this.router.navigate(['/']);
  }
}

