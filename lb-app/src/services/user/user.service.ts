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
 
  constructor(private http: HttpClient, private route: ActivatedRoute,private localStorageService: LocalStorageService) { }

  Login(login: Login) : Observable<any>{
    const apiUrl = `${this.url}/login`;
    return this.http.post<Token>(apiUrl, login, httpOptions).pipe(
      catchError((error: HttpErrorResponse) => this.handleError(error, "E-mail or Password wrong!"))
    );
  }

  Create(user: User) : Observable<any>{
    const apiUrl = `${this.url}/create`;
    return this.http.post<any>(apiUrl, user, httpOptions).pipe(
      catchError((error: HttpErrorResponse) => this.handleError(error, "Error to create user!"))
    );
  }

  RecoverPass(emailAddress: string): Observable<any> {
    const apiUrl = `${this.url}/recoverPassword`;
    return this.http.post<any>(apiUrl, '"' + emailAddress + '"', httpOptions).pipe(
      catchError((error: HttpErrorResponse) => this.handleError(error, "E-mail address not found!"))
    );
  }

  Authenticated() : Observable<any>{
    const httpOptionsAuth ={
      headers: new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer '+this.localStorageService.get("token")
      })
    }
    const apiUrl = `${this.url}/authenticated`;
    return this.http.get<any>(apiUrl, httpOptionsAuth).pipe(
      catchError((error: HttpErrorResponse) => this.handleError(error, "Error to Authenticate!"))
    ); 
  }

  UpdatePassword(password: string) : Observable<any>{
    let codeRecover = null;
    this.route.queryParams.subscribe(params => {
       codeRecover= params['codeRecover'];
    });

    const apiUrl = `${this.url}/updatePassword/${codeRecover}`;
    return this.http.post<any>(apiUrl, '"'+password+'"', httpOptions).pipe(
      catchError((error: HttpErrorResponse) => this.handleError(error, "Error to Update Password!"))
    );
  }

  private handleError(error: HttpErrorResponse, customErrorMessage: string) {
    if (error.error instanceof ErrorEvent) {
      console.error('Ocorreu um erro:', error.error.message);
    } else {
      console.error(
        `Código do erro ${error.status}, ` +
        `erro: ${JSON.stringify(error.error)}`);
    }
    return throwError(customErrorMessage);
  }

}
