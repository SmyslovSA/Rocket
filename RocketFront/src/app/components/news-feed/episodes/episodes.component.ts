import { Component, OnInit } from '@angular/core';
import { EpisodesPage } from '../../../models/news-feed/episodes-page';
import { NewsFeedService } from '../../../services/news-feed.service';

@Component({
  selector: 'app-episodes',
  templateUrl: './episodes.component.html',
  styleUrls: ['./episodes.component.css']
})
export class EpisodesComponent implements OnInit {

  episodesPage: EpisodesPage;

  constructor(private newsService: NewsFeedService) { }

  ngOnInit() {
  }

  onPageChanged(page: number) {
    this.newsService.getNewEpisodes(page)
       .subscribe(data => this.episodesPage = data);
  }

}
