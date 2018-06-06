import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaderResponse, HttpErrorResponse} from '@angular/common/http';
import { Profile } from '../models/personal-area/profile';
import { Observable } from 'rxjs';
import { Email } from '../models/personal-area/email';
import { GenreMusic } from '../models/personal-area/genreMusic';
import { GenreTv } from '../models/personal-area/genreTv';

@Injectable()
export class DataService {
    constructor(private http: HttpClient) {}
    getData(): Observable<Profile> {
       return this.http.get<Profile>('http://localhost:63613/personal/user/1');
    }

    getListGenreMusic(): Observable<GenreMusic> {
        return this.http.get<GenreMusic>('http://localhost:63613/genres/all/music');
     }

     getListGenreTv(): Observable<GenreTv> {
        return this.http.get<GenreTv>('http://localhost:63613/genres/all/tv');
     }
    changeData(user: Profile): Observable<Profile> {
       return this.http.put<Profile>(
           `http://localhost:63613/personal/user/info/1?firstName=${user.FirstName}&lastName=${user.LastName}&avatar=${user.Avatar}`, null);
    }

    changePassword(password: string, passwordConfirm: string): Observable<Profile> {
        return this.http.put<Profile>(
            `http://localhost:63613/personal/user/password/1?password=${password}&passwordConfirm=${passwordConfirm}`, null);
    }

    addemail(email: Email): Observable<Email> {
         return this.http.post<Email>(`http://localhost:63613/personal/email/add?id=1`, email);
    }

    deleteemail(id: number): Observable<HttpErrorResponse> {
       return this.http.delete<HttpErrorResponse>(`http://localhost:63613/personal/email/delete/${id}`);
    }
    addMusicGenre(user: Profile, genre: string): Observable<HttpErrorResponse> {
        return this.http.post<HttpErrorResponse>(`http://localhost:63613/personal/genres/music/add?id=${user.Id}&genre=${genre}`, null);
    }
    deleteMusicGenre(user: Profile, genre: string): Observable<HttpErrorResponse> {
        return this.http.post<HttpErrorResponse>(`http://localhost:63613/personal/genres/music/delete?id=${user.Id}&genre=${genre}`, null);
    }

    addTvGenre(user: Profile, genre: string): Observable<HttpErrorResponse> {
        return this.http.post<HttpErrorResponse>(`http://localhost:63613/personal/genres/tv/add?id=${user.Id}&genre=${genre}`, null);
    }
    deletTvGenre(user: Profile, genre: string): Observable<HttpErrorResponse> {
        return this.http.post<HttpErrorResponse>(`http://localhost:63613/personal/genres/tv/delete?id=${user.Id}&genre=${genre}`, null);
    }
}
