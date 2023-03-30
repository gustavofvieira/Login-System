import { HttpClient, HttpHeaders,HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from 'src/app/components/login/login';
import { Token } from 'src/app/components/login/token';

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
  constructor(private http: HttpClient) { }

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
}
