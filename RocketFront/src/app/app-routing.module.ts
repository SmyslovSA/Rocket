import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { AppComponent } from './app.component';
import { AdminComponent } from './components/admin/admin.component';
import { DonateComponent } from './components/donate/donate.component';
import { NewsFeedComponent } from './components/news-feed/news-feed.component';
import { PersonalAreaComponent } from './components/personal-area/personal-area.component';
import { RegistrationComponent } from './components/registration/registration.component';

const routes: Routes = [
  { path: '', component: AppComponent },
  { path: 'login', component: LoginComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'donate', component: DonateComponent },
  { path: 'news', component: NewsFeedComponent },
  { path: 'personal', component: PersonalAreaComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: '**', redirectTo: '' }
  // добавить путь для ошибки, обычные пути
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
