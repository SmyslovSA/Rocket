import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { SimpleUser } from '../models/personal-area/authorised-user';
import { error } from '@angular/compiler/src/util';
import {Observable} from 'rxjs';
import { map } from 'rxjs/operators';
@Injectable()
export class HttpService {
    private user: SimpleUser = new SimpleUser();
    constructor(private http: HttpClient) {}
    getData() {
         return this.http.get('http://localhost:63613/personal/user/1');
    }
}
