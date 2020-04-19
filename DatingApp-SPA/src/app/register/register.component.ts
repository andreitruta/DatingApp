import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { runInThisContext } from 'vm';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private autService: AuthService) { }

  ngOnInit() {
  }

  register(){
    this.autService.register(this.model).subscribe(() => {
      console.log('registration succesful');
    }, error =>{
      console.log(error);
    });
  }

  cancel(){
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }

}
