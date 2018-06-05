import { Component, OnInit } from '@angular/core';
import { Genre } from '../../../models/news-feed/genre';
import { NewsFeedService } from '../../../services/news-feed.service';
import { ActivatedRoute, Router } from '@angular/router';
import { GenreService } from '../../../services/genre.service';

@Component({
  selector: 'app-genres',
  templateUrl: './genres.component.html',
  styleUrls: ['./genres.component.css']
})
export class GenresComponent implements OnInit {

  selectedGenre: Genre;
  genres: Genre[];
  type: string;

  constructor(private newsService: NewsFeedService,
    private route: ActivatedRoute,
    private router: Router,
    private genreService: GenreService) { }

  ngOnInit() {
    this.type = this.route.snapshot.firstChild.url.pop().path;
    this.getGenres();
  }

  onGenreParamChanged(genreId: number) {
    this.selectedGenre = this.genres.find(x => x.Id === genreId);
    this.genreService.setGenre(this.selectedGenre);
  }

  getGenres() {
    this.newsService.getGenres(this.type)
       .subscribe(data => {
         this.genres = data;
         this.route.queryParamMap.subscribe(params =>
          this.onGenreParamChanged(+params.get('genre')));
       });
  }

  setGenre(genre: Genre) {
    this.selectedGenre = genre;
    this.router.navigate([], { queryParams: { genre: genre.Id, page: 1 } });
  }

  clearGenre() {
    this.selectedGenre = null;
    this.router.navigate([], { queryParams: { page: 1 } });
  }
}
