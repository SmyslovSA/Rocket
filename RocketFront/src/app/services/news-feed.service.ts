import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EpisodesPage } from '../models/news-feed/episodes-page';
import { MusicPage } from '../models/news-feed/music-page';
import { SeriesPage } from '../models/news-feed/series-page';

@Injectable({
  providedIn: 'root'
})
export class NewsFeedService {

  constructor(private http: HttpClient) { }

  getNewEpisodes(page: number): Observable<EpisodesPage> {
    return this.http.get<EpisodesPage>(`http://localhost:63613/episode/new/page_${page}?page_size=12`);
  }

  getNewMusic(page: number): Observable<MusicPage> {
    return this.http.get<MusicPage>(`http://localhost:63613/music/page/${page}`);
  }

  getSeriesPage(page: number): Observable<SeriesPage> {
    return this.http.get<SeriesPage>(`http://localhost:63613/tvseries/page_${page}?page_size=12`);
  }
}
