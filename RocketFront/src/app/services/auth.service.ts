import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Subject } from 'rxjs';
import { debug } from 'util';

@Injectable()
export class RocketAuthService {
  authSubj: Subject<boolean>; // subscribe inside another components

  constructor(private openId: OAuthService) { }

  login(username: string, password: string) {
    if (this.IsAuthenticated) {
      this.openId.logOut();
      this.authSubj.next(false);
      // redirect to login
    }

    this.openId.fetchTokenUsingPasswordFlow(username, password)
      .then(result => this.authSubj.next(true))
      .catch(ex => console.log(ex));
  }

  get IsAuthenticated(): boolean {
    // add token actual date
    return this.openId.getAccessToken() != null
      && this.openId.getAccessTokenExpiration() > Date.now();
  }
}
