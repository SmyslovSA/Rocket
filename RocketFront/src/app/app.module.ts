import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule }    from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { SignalRComponent } from './components/signalR/signalR.component';

import { AppRoutingModule } from './app-routing.module';
import { MenuComponent } from './components/menu/menu.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { NotificationComponent } from './components/notification/notification.component';
import { AdminComponent } from './components/admin/admin.component';
import { PaymentComponent } from './components/payment/payment.component';
import { DonateComponent } from './components/donate/donate.component';
import { NewsFeedComponent } from './components/news-feed/news-feed.component';
import { PersonalAreaComponent } from './components/personal-area/personal-area.component';
import { SignalRModule } from 'ng2-signalr';
import { SignalRConfiguration } from 'ng2-signalr';
import { SnotifyModule, SnotifyService, ToastDefaults } from 'ng-snotify';

export function createConfig(): SignalRConfiguration {
  const c = new SignalRConfiguration();
  c.hubName = 'Notification';
  c.url = 'http://localhost:63613/';
  c.logging = true;
  
  return c;
}


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignalRComponent,
    MenuComponent,
    RegistrationComponent,
    NotificationComponent,
    AdminComponent,
    PaymentComponent,
    DonateComponent,
    NewsFeedComponent,
    PersonalAreaComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    SignalRModule.forRoot(createConfig),
    SnotifyModule
  ],
  providers: [{ provide: 'SnotifyToastConfig', useValue: ToastDefaults},  SnotifyService],
  bootstrap: [AppComponent]
})
export class AppModule { }
