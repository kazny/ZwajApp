import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

@Input() valuesFromRegister:any;
@Output() cancelRegister = new EventEmitter();
model:any = {};
  constructor(private authService : AuthService,private alertifyService:AlertifyService) { }

  ngOnInit() {
  }
  cancel(){
    this.cancelRegister.emit(false);
  }
  register(){
    this.authService.Register(this.model).subscribe(
      ()=>{this.alertifyService.success("مرحبا بك في رواج وت نت")},
      error=> { this.alertifyService.error(error)}
    )
  }
}
