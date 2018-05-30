import { Component, OnInit } from '@angular/core';
import { SignalR, SignalRConnection, IConnectionOptions, BroadcastEventListener } from 'ng2-signalr';
import {SnotifyService} from 'ng-snotify';

@Component({
    selector: 'rocket-signalR',
    template: ''
})

export class SignalRComponent implements OnInit {
    
    constructor(private _signalR: SignalR, private snotifyService: SnotifyService) {
    } 
 
    ngOnInit() {
        this.connect();
    }

    connect() {
        let o: IConnectionOptions;
        let conx = this._signalR.createConnection();
        conx.status.subscribe((s) => console.warn(s.name));
        conx.start().then((c) => {
    
          let listener = c.listenFor('notifyAll');
          listener.subscribe(data => {
              this.snotifyService.simple(data.toString(),"Новый релиз!");
                  console.log(data);
    
          });
        });
      }  
}