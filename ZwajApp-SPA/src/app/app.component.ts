import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  jwtHelper = new JwtHelperService();
  decodedToToken : any;

constructor(private authService:AuthService) {}
  ngOnInit() {
    this.authService.decodedToToken=this.jwtHelper.decodeToken(localStorage.getItem('token'));
  }
}
