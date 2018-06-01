import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { DonateComponent } from './components/donate/donate.component';
import { NewsFeedComponent } from './components/news-feed/news-feed.component';
import { PersonalAreaComponent } from './components/personal-area/personal-area.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { CalendarComponent } from './components/calendar/calendar.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'donate', component: DonateComponent },
  { path: 'news/:type', component: NewsFeedComponent },
  { path: 'personal', component: PersonalAreaComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'calendar', component: CalendarComponent },
  { path: '', redirectTo: '/news/episodes', pathMatch: 'full' },
  { path: '**', redirectTo: '' }
  // добавить путь для ошибки, обычные пути
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
