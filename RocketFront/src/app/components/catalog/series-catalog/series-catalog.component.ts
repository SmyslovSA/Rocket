import { Component, OnInit } from '@angular/core';
import { NewsFeedService } from '../../../services/news-feed.service';
import { SeriesPage } from '../../../models/news-feed/series-page';

@Component({
  selector: 'app-series-catalog',
  templateUrl: './series-catalog.component.html',
  styleUrls: ['./series-catalog.component.css']
})
export class SeriesCatalogComponent implements OnInit {

  seriesPage: SeriesPage;

  constructor(private newsService: NewsFeedService) { }

  ngOnInit() {
  }

  onPageChanged(page: number) {
    this.newsService.getSeriesPage(page)
       .subscribe(data => this.seriesPage = data);
  }

}
