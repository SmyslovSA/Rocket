import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent }   from './components/login/login.component';

const routes: Routes = [
  { path: '', redirectTo: '/components/login', pathMatch: 'full' },
  //добавить путь для ошибки, обычные пути
];
 
@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
