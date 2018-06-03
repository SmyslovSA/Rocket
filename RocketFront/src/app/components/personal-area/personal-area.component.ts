import { Component, OnInit } from '@angular/core';
import {SimpleUser} from '../../models/personal-area/simpleuser';
import {HttpClientModule} from '@angular/common/http';
import { DataService } from '../../services/simpleuser.data.service';

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
  }
  changeUserInfo() {
    this.dataservice.changeData(this.data).subscribe();
  }
  changeUserPassword(password: string, passwordConfirm: string) {
    this.dataservice.changePassword(password, passwordConfirm).subscribe();
  }
  ngOnInit() {
    this.dataservice.getData().subscribe(data => this.data = data);
  }
}
