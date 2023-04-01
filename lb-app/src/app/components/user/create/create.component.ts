import { Component, OnInit, TemplateRef, Inject, LOCALE_ID  } from '@angular/core';
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

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl(null),
      emailAddress: new FormControl(null),
      password: new FormControl(null),
      role: new FormControl("Common"),

    });
  }

  Send(): void {
    const user: User = this.form.value;
    console.log(user)
      this.userService.Create(user).subscribe((result) => {
        alert(result);
        this.Back()
      });
  }

  Back(): void {
    this.router.navigate(['/']);
  }
}

