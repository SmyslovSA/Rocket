import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { DonateComponent } from './components/donate/donate.component';
import { NewsFeedComponent } from './components/news-feed/news-feed.component';
import { PersonalAreaComponent } from './components/personal-area/personal-area.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'donate', component: DonateComponent },
  { path: 'news', component: NewsFeedComponent },
  { path: 'personal', component: PersonalAreaComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'calendar', component: CalendarComponent },
  { path: 'users', component: UsersComponent},
  { path: '', redirectTo: '/news', pathMatch: 'full' },
  { path: '**', redirectTo: '' }
  // добавить путь для ошибки, обычные пути
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
