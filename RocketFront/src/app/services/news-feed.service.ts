import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EpisodesPage } from '../models/news-feed/episodes-page';
import { MusicPage } from '../models/news-feed/music-page';
import { Music } from '../models/news-feed/music';
import { SeriesPage } from '../models/news-feed/series-page';
import { SeriesDetails } from '../models/news-feed/series-details';
import { Genre } from '../models/news-feed/genre';
import { MusicDetails } from '../models/news-feed/music-details';

@Injectable({
  providedIn: 'root'
})
export class NewsFeedService {

  subscribeOnRelease(id: number): Observable<any> {
    return this.http.put<any>(`http://localhost:63613/subscribe/${id}`, null);
  }
  constructor(private http: HttpClient) { }

  getNewEpisodes(page: number, genre: number): Observable<EpisodesPage> {
    return this.http.get<EpisodesPage>(`http://localhost:63613/episode/new/page_${page}?page_size=12${genre ? '&genre_id=' + genre : ''}`);
  }

  getNewMusic(page: number, genre): Observable<MusicPage> {
    return this.http.get<MusicPage>(`http://localhost:63613/music/page/${page}${genre ? '?genreId=' + genre : ''}`);
  }

  getMusicDetails(id: number): Observable<MusicDetails> {
    return this.http.get<MusicDetails>(`http://localhost:63613/music/${id}`);
  }

  getSeriesPage(page: number, genre: number): Observable<SeriesPage> {
    return this.http.get<SeriesPage>(`http://localhost:63613/tvseries/page_${page}?page_size=12${genre ? '&genre_id=' + genre : ''}`);
  }

  getSeriesDetails(id: number): Observable<SeriesDetails> {
    return this.http.get<SeriesDetails>(`http://localhost:63613/tvseries/${id}?episodes_count=5&persons_count=5`);
  }

  getGenres(type: string): Observable<Genre[]> {
    type = type.replace('episodes', 'series');
    return this.http.get<Genre[]>(`http://localhost:63613/genre/${type}/all`);
  }
}
