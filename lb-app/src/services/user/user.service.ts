import { HttpClient, HttpHeaders,HttpInterceptor, HttpHandler, HttpRequest,HttpErrorResponse  } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Login } from 'src/app/components/user/login/login';
import { Token } from 'src/app/components/user/login/token';
import { User } from 'src/app/components/user/create/user';
import { ActivatedRoute } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { LocalStorageService } from 'src/services/local-storage.service';


const httpOptions ={
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url = 'https://localhost:7141/v1/user';
  message ="";
 
  constructor(private http: HttpClient, private route: ActivatedRoute,private localStorageService: LocalStorageService) { }

  Login(login: Login) : Observable<any>{
    const apiUrl = `${this.url}/login`;
    var response = this.http.post<Token>(apiUrl, login, httpOptions)
    return response;
  }

  Create(user: User) : Observable<any>{
    const apiUrl = `${this.url}/create`;
    return this.http.post<any>(apiUrl, user, httpOptions)
  }

  // RecoverPass(emailAddress: string) : Observable<any>{
  //   const apiUrl = `${this.url}/recoverPassword`;
  //   return this.http.post<any>(apiUrl, '"'+emailAddress+'"', httpOptions) 
  // }

  RecoverPass(emailAddress: string) : Observable<any>{
    this.setMessage("E-mail address not found!");
    const apiUrl = `${this.url}/recoverPassword`;
    return this.http.post<any>(apiUrl, '"'+emailAddress+'"', httpOptions).pipe(
      catchError(this.handleError)
    );
    }

    setMessage(message: string)
    {
      return this.message = message;
    }

    getMessage()
    {
      return this.message;
    }

    private handleError(error: HttpErrorResponse) {
      console.log("handleError: ",error)
      if (error.error instanceof ErrorEvent) {
        console.error('Ocorreu um erro:', error.error.message);
      } else {
        console.error(
          `Código do erro ${error.status}, ` +
          `erro: ${JSON.stringify(error.error)}`);
      }
      return throwError(
        this.getMessage());
    }




  Authenticated() : Observable<any>{
    const httpOptionsAuth ={
      headers: new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer '+this.localStorageService.get("token")
      })
    }
    const apiUrl = `${this.url}/authenticated`;
    return this.http.get<any>(apiUrl, httpOptionsAuth) 
  }

  UpdatePassword(password: string) : Observable<any>{
    let codeRecover = null;
    this.route.queryParams.subscribe(params => {
       codeRecover= params['codeRecover'];
    });

    const apiUrl = `${this.url}/updatePassword/${codeRecover}`;
    return this.http.post<any>(apiUrl, '"'+password+'"', httpOptions) 
  }

}
