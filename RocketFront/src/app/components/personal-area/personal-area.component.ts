import { Component, OnInit } from '@angular/core';
import {SimpleUser} from '../../models/personal-area/simpleuser';
import {HttpClientModule} from '@angular/common/http';
import { DataService } from '../../services/simpleuser.data.service';
import { empty } from 'rxjs';

@Component({
  selector: 'app-personal-area',
  templateUrl: './personal-area.component.html',
  styleUrls: ['./personal-area.component.css'],
  providers: [DataService]
})
export class PersonalAreaComponent implements OnInit {
  data: SimpleUser;
  constructor(private dataservice: DataService) {
    this.data = new SimpleUser();
    this.data.FirstName = '';
    this.data.LastName = '';
    this.data.Avatar = '';
  }
  changeUserInfo() {
    this.dataservice.changeData(this.data).subscribe(data => this.data = data);
  }
  changeUserPassword(password: string, passwordConfirm: string) {
    this.dataservice.changePassword(password, passwordConfirm).subscribe(data => this.data = data);
  }
  ngOnInit() {
    this.dataservice.getData().subscribe(data => this.data = data);
  }
}
