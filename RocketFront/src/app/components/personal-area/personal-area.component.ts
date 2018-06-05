import { Component, OnInit } from '@angular/core';
import {Profile} from '../../models/personal-area/profile';
import {HttpClientModule} from '@angular/common/http';
import { DataService } from '../../services/profile.data.service';
import { Email } from '../../models/personal-area/email';
import {HttpHeaderResponse} from '@angular/common/http';

@Component({
  selector: 'app-personal-area',
  templateUrl: './personal-area.component.html',
  styleUrls: ['./personal-area.component.css'],
  providers: [DataService]
})
export class PersonalAreaComponent implements OnInit {
  data: Profile;
  testemail: Email;
  constructor(private dataservice: DataService) {
    this.data = new Profile();
  }
  changeUserInfo() {
    this.dataservice.changeData(this.data).subscribe();
  }
  changeUserPassword(password: string, passwordConfirm: string) {
    this.dataservice.changePassword(password, passwordConfirm).subscribe();
  }
  addEmail(newEmail: string) {
    const addemail =  new Email();
    addemail.Name = newEmail;
    this.dataservice.addemail(addemail).subscribe(data => {
    addemail.Id = +(data.headers.getAll('Location'));
    this.data.Emails.push(addemail);
  });
  }
  deleteEmail(deleteEmailId: number) {
    this.dataservice.deleteemail(deleteEmailId).subscribe();
  }
  ngOnInit() {
    this.dataservice.getData().subscribe(data => this.data = data);
  }
}
