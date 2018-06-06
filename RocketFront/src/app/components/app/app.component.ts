import { Component } from '@angular/core';
import { OAuthService, AuthConfig, JwksValidationHandler } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  constructor(private oidServ: OAuthService) {
    this.ConfigureOAuth();
  }

  private ConfigureOAuth() {
    this.oidServ.configure(authConfig);
    this.oidServ.tokenValidationHandler = new JwksValidationHandler();
    this.oidServ.loadDiscoveryDocumentAndLogin();
  }
}

export const authConfig: AuthConfig = {
  issuer: 'url to issuer here',
  showDebugInformation: true, // remove after debug
  clientId: 'client id here',
  dummyClientSecret: 'client secret here',
  scope: 'openid profile email phone custom',
  oidc: false
};
