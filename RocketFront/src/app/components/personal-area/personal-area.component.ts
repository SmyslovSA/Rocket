import { Component, OnInit } from '@angular/core';
import {Profile} from '../../models/personal-area/profile';
import {HttpClientModule, HttpErrorResponse, HttpResponse} from '@angular/common/http';
import { DataService } from '../../services/profile.data.service';
import { Email } from '../../models/personal-area/email';

@Component({
  selector: 'app-personal-area',
  templateUrl: './personal-area.component.html',
  styleUrls: ['./personal-area.component.css'],
  providers: [DataService]
})
export class PersonalAreaComponent implements OnInit {
  data: Profile;
  errorMessage: string;
  constructor(private dataservice: DataService) {
    this.data = new Profile();
  }

  changeUserInfo() {
    this.dataservice.changeData(this.data).subscribe();
  }

  changeUserPassword(password: string, passwordConfirm: string) {
    this.dataservice.changePassword(password, passwordConfirm).subscribe();
    // добавить reqExp на ввод пароля
    // проверить привязки в html
  }

  addEmail(newEmail: string) {
    const addemail =  new Email();
    addemail.Name = newEmail;
    this.dataservice.addemail(addemail).subscribe
    ((data) => {
      addemail.Id = data.Id;
      this.data.Emails.push(addemail);
      }, (err: HttpErrorResponse) => {
        this.errorMessage = err.message;
      });
      // необходимо errorMessage выводить на фронт + скрывать после следующего нажатия на кнопку
      // добавить reqExp на ввод e-mail
      // проверить привязки к модели в html
  }

  deleteEmail(deleteEmailId: number) {
    this.dataservice.deleteemail(deleteEmailId).subscribe(data => console.log(data.message));
  }

  ngOnInit() {
    this.dataservice.getData().subscribe(data => this.data = data);
  }
}
