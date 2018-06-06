import { Component, OnInit } from '@angular/core';
import { SignalR, SignalRConnection, IConnectionOptions, BroadcastEventListener } from 'ng2-signalr';
import {SnotifyService} from 'ng-snotify';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  currTemplate: string;
  pushMsg: string = '';

  constructor(private _signalR: SignalR, private snotifyService: SnotifyService)  {

  }

  ngOnInit(){
    this.showTable('1');
  }

  sendPush(){
    if (this.pushMsg.length > 0) {
      let conx = this._signalR.createConnection();
      conx.status.subscribe((s) => console.warn(s.name));
      conx.start().then((c) => {  
        conx.invoke('sendMessage', this.pushMsg).then(() => {
          this.pushMsg = '';
        });
      });
    }    
  }

  showTable(templateId: string){
    this.currTemplate = templateId;

    switch(this.currTemplate) { 
      case '1': { 
         //statements; 
         break; 
      } 
      case '2': { 
         //statements; 
         break; 
      } 
      case '3': { 
        //statements; 
        break; 
     } 
      default: { 
         //statements; 
         break; 
      } 
   } 

  }



}