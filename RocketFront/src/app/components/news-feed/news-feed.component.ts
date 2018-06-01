import { Component, OnInit } from '@angular/core';
import { NewsFeedService } from '../../services/news-feed.service';
import { SeriesPage } from '../../models/news-feed/series-page';
import { ActivatedRoute, Router } from '@angular/router';
import 'rxjs/add/operator/filter';
import { Observable } from 'rxjs/internal/Observable';
import { MusicPage } from '../../models/news-feed/music-page';

@Component({
  selector: 'app-news-feed',
  templateUrl: './news-feed.component.html',
  styleUrls: ['./news-feed.component.css']
})
export class NewsFeedComponent implements OnInit {

  type: string;
  page: number;
  genre: number;
  seriesPage: SeriesPage;
  musicPage: MusicPage;

  constructor(private newsService: NewsFeedService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.type = this.route.snapshot.paramMap.get('type') || 'episodes';
    this.page = +(this.route.snapshot.queryParamMap.get('page') || 1);
    this.genre = +this.route.snapshot.queryParamMap.get('type');
    this.getPage(this.page);
    this.getNews();
  }

  getNews() {
    switch (this.type) {
      case 'episodes':
      this.newsService.getNewEpisodes(this.page)
      .subscribe(data => this.seriesPage = data);
        break;
        case 'music':
        this.newsService.getNewMusic(this.page)
        .subscribe(data => this.musicPage = data);
          break;
      default:
      // todo not found
        break;
    }
  }

  getPage(page: number) {
    this.page = page;
    this.router.navigate(['news', this.type], { queryParams: { page: this.page } });
    this.getNews();
  }

}
