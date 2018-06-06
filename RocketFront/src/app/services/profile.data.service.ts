import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaderResponse} from '@angular/common/http';
import { Profile } from '../models/personal-area/profile';
import { Observable } from 'rxjs';
import { Email } from '../models/personal-area/email';

@Injectable()
export class DataService {
    constructor(private http: HttpClient) {}
    getData(): Observable<Profile> {
       return this.http.get<Profile>('http://localhost:63613/personal/user/1');
    }

    changeData(user: Profile): Observable<Profile> {
       return this.http.put<Profile>(
           `http://localhost:63613/personal/user/info/1?firstName=${user.FirstName}&lastName=${user.LastName}&avatar=${user.Avatar}`, null);
    }

    changePassword(password: string, passwordConfirm: string): Observable<Profile> {
        return this.http.put<Profile>(
            `http://localhost:63613/personal/user/password/1?password=${password}&passwordConfirm=${passwordConfirm}`, null);
    }

    addemail(email: Email): Observable<HttpHeaderResponse> {
        return this.http.post<HttpHeaderResponse>(`http://localhost:63613/personal/email/add?id=1`, email);
    }

    deleteemail(id: number) {
       return this.http.delete(`http://localhost:63613/personal/email/delete/${id}`);
    }
}
