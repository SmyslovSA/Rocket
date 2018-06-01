import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SeriesPage } from '../models/news-feed/series-page';
import { MusicPage } from '../models/news-feed/music-page';

@Injectable({
  providedIn: 'root'
})
export class NewsFeedService {

  constructor(private http: HttpClient) { }

  getNewEpisodes(page: number): Observable<SeriesPage> {
    return this.http.get<SeriesPage>(`http://localhost:63613/episode/new/page_${page}?page_size=12`);
  }

  getNewMusic(page: number): Observable<MusicPage> {
    return this.http.get<MusicPage>(`http://localhost:63613/music/page/${page}`);
  }
}
