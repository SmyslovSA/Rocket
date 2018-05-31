import { Component, OnInit } from '@angular/core';
import { SimpleUser } from '../../models/personal-area/authorised-user';
import {HttpClientModule} from '@angular/common/http';
import { DataService } from '../../services/authoriseduser.data.service';
import { HttpService } from '../../services/http.service';
import { error } from 'protractor';

@Component({
  selector: 'app-personal-area',
  templateUrl: './personal-area.component.html',
  styleUrls: ['./personal-area.component.css'],
  providers: [HttpService, DataService]
})
export class PersonalAreaComponent implements OnInit {
  data: SimpleUser = new SimpleUser();
  constructor(private http: HttpService, private dataservice: DataService) { }
  changeUserInfo() {
  }
  ngOnInit() {
      this.http.getData().subscribe((data: SimpleUser) => {
      this.data = data;
  });
   // this.data = this.dataservice.getData();
  }
}
