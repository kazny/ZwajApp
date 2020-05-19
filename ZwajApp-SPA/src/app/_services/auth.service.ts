import { from } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  jwtHelper = new JwtHelperService();
  decodedToToken : any;
  BaseUrl: string = 'http://localhost:5000/api/auth/';
  decodedTo:any;
  constructor(private http: HttpClient) {}
  Login(model: any) {
    return this.http.post(this.BaseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToToken=this.jwtHelper.decodeToken(user.token);
          console.log(this.decodedToToken);
        }
      })
    );
  }
  Register(model: any) {
    return this.http.post(this.BaseUrl + 'register', model);
  }
  loggedIn()
  {
    try{
      const token =localStorage.getItem('token');
    return ! this.jwtHelper.isTokenExpired(token);
    }catch{
      return false;
    }

  }
}
