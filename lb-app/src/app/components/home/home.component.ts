import { Component, OnInit, Inject, LOCALE_ID  } from '@angular/core';
import { LocalStorageService } from 'src/services/local-storage.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit{

  constructor(@Inject(LOCALE_ID) public locale: string, private route: ActivatedRoute,private localStorageService: LocalStorageService,
  private router: Router) {}

  welcome: string | undefined;

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.welcome = params['welcome'];
    });
  }

  SignOut(): void {
    this.localStorageService.remove("token");
    this.router.navigate(['/']);
  }
}

