import { Component, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode : boolean=false;
  values:any;
  constructor(private http:HttpClient) { }

  ngOnInit() {
    this.values=this.getValues();
  }
  registerToggle()
  {
    this.registerMode=!this.registerMode;
    console.log(this.registerMode);
  }
  getValues() {
    this.http.get('http://localhost:5000/api/WeatherForecast').subscribe(
      response=>{this.values=response},
      error=>{console.log(error);}
      )
  }
  cancelRegisterParent(mode:boolean){
    console.log(mode);
    this.registerMode=mode;
  }
}
