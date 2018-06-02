import { Component, OnInit } from '@angular/core';
import { MusicPage } from '../../../models/news-feed/music-page';
import { NewsFeedService } from '../../../services/news-feed.service';

@Component({
  selector: 'app-musics',
  templateUrl: './musics.component.html',
  styleUrls: ['./musics.component.css']
})
export class MusicsComponent implements OnInit {

  musicPage: MusicPage;

  constructor(private newsService: NewsFeedService) { }

  ngOnInit() {
  }

  onPageChanged(page: number) {
    this.newsService.getNewMusic(page)
       .subscribe(data => this.musicPage = data);
  }

}
