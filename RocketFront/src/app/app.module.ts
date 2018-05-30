import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule }    from '@angular/forms';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { SignalR } from './components/signalR/signalR.component';

import { AppRoutingModule } from './app-routing.module';
import { MenuComponent } from './components/menu/menu.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { NotificationComponent } from './components/notification/notification.component';
import { AdminComponent } from './components/admin/admin.component';
import { PaymentComponent } from './components/payment/payment.component';
import { DonateComponent } from './components/donate/donate.component';
import { NewsFeedComponent } from './components/news-feed/news-feed.component';
import { PersonalAreaComponent } from './components/personal-area/personal-area.component';
import { CalendarComponent } from './components/calendar/calendar.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignalR,
    MenuComponent,
    RegistrationComponent,
    NotificationComponent,
    AdminComponent,
    PaymentComponent,
    DonateComponent,
    NewsFeedComponent,
    PersonalAreaComponent,
    CalendarComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
