import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { SimpleUser } from '../models/personal-area/simpleuser';
import { Observable } from 'rxjs';

@Injectable()
export class DataService {
    constructor(private http: HttpClient) {}
    getData(): Observable<SimpleUser> {
       return this.http.get<SimpleUser>('http://localhost:63613/personal/user/1');
    }

    changeData(user: SimpleUser): Observable<SimpleUser> {
       return this.http.put<SimpleUser>(
           `http://localhost:63613/personal/user/info/1?firstName=${user.FirstName}&lastName=${user.LastName}&avatar=${user.Avatar}`, null);
    }

    changePassword(password: string, passwordConfirm: string): Observable<SimpleUser> {
        return this.http.put<SimpleUser>(
            `http://localhost:63613/personal/user/password/1?password=${password}&passwordConfirm=${passwordConfirm}`, null);
    }
}
