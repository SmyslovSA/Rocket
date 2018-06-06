import { Component, OnInit } from '@angular/core';
import { Genre } from '../../../models/news-feed/genre';
import { NewsFeedService } from '../../../services/news-feed.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-genres',
  templateUrl: './genres.component.html',
  styleUrls: ['./genres.component.css']
})
export class GenresComponent implements OnInit {

  genres: Genre[];
  type: string;

  constructor(private newsService: NewsFeedService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.type = this.route.snapshot.firstChild.url.pop().path;
    this.getGenres();
  }

  getGenres() {
    this.newsService.getGenres(this.type)
       .subscribe(data => this.genres = data);
  }

  setGenre(id: number) {
    this.router.navigate([], { queryParams: { genre: id }, queryParamsHandling: 'preserve' });
  }
}
