import { Component, OnInit,ChangeDetectorRef } from '@angular/core';
import {Profile} from '../../models/personal-area/profile';
import {HttpClientModule, HttpErrorResponse, HttpResponse} from '@angular/common/http';
import { DataService } from '../../services/profile.data.service';
import { Email } from '../../models/personal-area/email';
import { listGenreMusic } from '../../models/personal-area/listGenreMusic';
import { GenreMusic } from '../../models/personal-area/genreMusic';
import { GenreTv } from '../../models/personal-area/genreTv';



@Component({
  selector: 'app-personal-area',
  templateUrl: './personal-area.component.html',
  styleUrls: ['./personal-area.component.css'],
  providers: [DataService]
})
export class PersonalAreaComponent implements OnInit {
 
  data: Profile;
  errorMessage: string;
  listGenreMisic:Array<GenreMusic>;
  listGenreTv:Array<GenreTv>;
  listGenreMusicForFront:Array<GenreMusic>;
  listGenreTvForFront:Array<GenreTv>;
  dataGenreMusicForFront:Array<string>;
  dataGenreTvForFront:Array<string>;
  visible:boolean=true;

  constructor(private dataservice: DataService,private changeDetectorRef: ChangeDetectorRef) {
    this.data = new Profile();
    this.listGenreMisic=new Array<GenreMusic>();
    this.listGenreTv=new Array<GenreTv>();
    this.listGenreMusicForFront=new Array<GenreMusic>();
    this.dataGenreMusicForFront=new Array<string>();
    this.dataGenreTvForFront=new Array<string>();
this.dataGenreMusicForFront=["ssss","asasasa"];
let gen=new GenreMusic();
gen.Name="qqqqq";
gen.Id=1;
this.listGenreMusicForFront.push(gen);
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
  addGenreMusic(selectedGenreMusic:string)
  {
    
   this.dataGenreMusicForFront.push(selectedGenreMusic);
    this.dataservice.addMusicGenre(this.data,selectedGenreMusic).subscribe(data => console.log(data.message));
  }
  deleteMusicGenre(genMusic:string,index:number)
  {
    this.dataservice.deleteMusicGenre(this.data,genMusic).subscribe(data => console.log(data.message));
   
    this.dataGenreMusicForFront.splice(index, 1);
    this.changeDetectorRef.detectChanges();
  }
  addGenreTv(selectedGenreTv:string)
  {
    this.dataGenreTvForFront.push(selectedGenreTv);
    this.dataservice.addTvGenre(this.data,selectedGenreTv).subscribe(data => console.log(data.message));
  }
  deleteTvGenre(genTv:string,index:number)
  {
    this.dataservice.deletTvGenre(this.data,genTv).subscribe(data => console.log(data.message));
    this.dataGenreTvForFront.splice(index, 1);
    this.changeDetectorRef.detectChanges();
  }
  ngOnInit() {
    this.dataservice.getData().subscribe(data => this.data = data);
    this.dataservice.getListGenreMusic().subscribe(listGenreMisic=>this.listGenreMisic.push(listGenreMisic));
    this.dataservice.getListGenreTv().subscribe(listGenreTv=>this.listGenreTv.push(listGenreTv));

   for(var obj of this.data.GenreMusic)
   {
     if(this.listGenreMisic.includes(obj))
     {
      this.listGenreMusicForFront.push(obj);
     }
   }
   for(var obj of this.data.GenreTv)
   {
     if(this.listGenreTv.includes(obj))
     {
      this.listGenreTvForFront.push(obj);
     }
   }
   for(var obj of this.data.GenreMusic)
   {
    this.dataGenreMusicForFront.push(obj.Name);
       }
   for(var obj of this.data.GenreMusic)
   {
    this.dataGenreTvForFront.push(obj.Name);
   }
  
  }
}
