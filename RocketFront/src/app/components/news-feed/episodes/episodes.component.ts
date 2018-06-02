import { Component, OnInit } from '@angular/core';
import { SeriesPage } from '../../../models/news-feed/series-page';
import { NewsFeedService } from '../../../services/news-feed.service';

@Component({
  selector: 'app-episodes',
  templateUrl: './episodes.component.html',
  styleUrls: ['./episodes.component.css']
})
export class EpisodesComponent implements OnInit {

  seriesPage: SeriesPage;

  constructor(private newsService: NewsFeedService) { }

  ngOnInit() {
  }

  onPageChanged(page: number) {
    this.newsService.getNewEpisodes(page)
       .subscribe(data => this.seriesPage = data);
  }

}
