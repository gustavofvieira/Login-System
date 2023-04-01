import { HttpClient, HttpHeaders,HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from 'src/app/components/user/login/login';
import { Token } from 'src/app/components/user/login/token';
import { User } from 'src/app/components/user/create/user';
import { ActivatedRoute } from '@angular/router';

const httpOptions ={
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class UserService implements HttpInterceptor {
  url = 'https://localhost:7141/v1/user';
  
  private token: string = '';
  
  setToken(token: string) {
    this.token = token;
  }
  constructor(private http: HttpClient, private route: ActivatedRoute) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    if (this.token) {
      const authReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${this.token}`)
      });
      return next.handle(authReq);
    }
    return next.handle(req);
  }


  Login(login: Login) : Observable<any>{
    const apiUrl = `${this.url}/login`;
    var response = this.http.post<Token>(apiUrl, login, httpOptions)
    console.log("response: ",response)
    return response;
  }

  Create(user: User) : Observable<any>{
    const apiUrl = `${this.url}/create`;
    return this.http.post<any>(apiUrl, user, httpOptions)
  }

  RecoverPass(emailAddress: string) : Observable<any>{
    console.log("service: ", emailAddress)
    const apiUrl = `${this.url}/recoverPassword`;
    return this.http.post<any>(apiUrl, '"'+emailAddress+'"', httpOptions) 
  }

  Authenticated(token: string) : Observable<any>{
    
    const httpOptionsAuth ={
      headers: new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer '+token
      })
    }

    const apiUrl = `${this.url}/authenticated`;

    return this.http.get<any>(apiUrl, httpOptionsAuth) 
  }

  UpdatePassword(password: string) : Observable<any>{
    let id = this.route.snapshot.paramMap.get('id');
    console.log("service: ", this.route)
    const apiUrl = `${this.url}/updatePassword/${id}`;
    return this.http.post<any>(apiUrl, '"'+password+'"', httpOptions) 
  }

}
