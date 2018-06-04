import { Component, OnInit } from '@angular/core';
import { NewsFeedService } from '../../../services/news-feed.service';
import { SeriesPage } from '../../../models/news-feed/series-page';
import { GenreService } from '../../../services/genre.service';
import { Genre } from '../../../models/news-feed/genre';

@Component({
  selector: 'app-series-catalog',
  templateUrl: './series-catalog.component.html',
  styleUrls: ['./series-catalog.component.css'],
})
export class SeriesCatalogComponent implements OnInit {

  seriesPage: SeriesPage;
  genre: Genre;

  constructor(private newsService: NewsFeedService, private genreService: GenreService) {
    this.genreService.genre.subscribe(data => {
      this.genre = data;
      this.onPageChanged(1, this.genre);
    });
  }

  ngOnInit() {
  }

  onPageChanged(page: number, genre = null) {
    this.newsService.getSeriesPage(page, this.genre)
       .subscribe(data => this.seriesPage = data);
  }

}
