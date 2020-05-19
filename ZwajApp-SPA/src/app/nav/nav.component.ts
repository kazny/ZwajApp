import { error } from '@angular/compiler/src/util';
import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model : any ={};
  constructor(public authService: AuthService
    ,private alertify : AlertifyService
    ,private router : Router) { }

  ngOnInit() {
  }
  Login (){

    this.authService.Login(this.model).subscribe(
      next => this.alertify.success("connected!!"),
      error=>this.alertify.error("faild"),
      () => { this.router.navigate(['/members'])}
    );
    // console.log(localStorage.getItem('token'));
  }
  loggedIn(){
    // const token =localStorage.getItem('token');
    // return!! token;
    return this.authService.loggedIn();
  }
  loggedOut(){
    localStorage.removeItem('token');
    this.alertify.message('loged out');
    this.router.navigate(['']);
  }
}
