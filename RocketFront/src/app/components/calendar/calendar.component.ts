import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { CalendarEvent } from 'angular-calendar';
import {
  isSameMonth,
  isSameDay,
  startOfMonth,
  endOfMonth,
  startOfWeek,
  endOfWeek,
  startOfDay,
  endOfDay,
  format
} from 'date-fns';
import { Observable } from 'rxjs';
import { colors } from './calendar-utils/colors';

interface Release {
  id: number;
  title: string;
  release_date: string;
}

interface ReleaseMusic {
  id: number;
  Title: string;
  ReleaseDate: string;
}

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  view: string = 'month';

  viewDate: Date = new Date();

  events2$: Observable<Array<CalendarEvent<{ release: ReleaseMusic }>>>;
  events$: Observable<Array<CalendarEvent<{ release: Release }>>>;
  

  activeDayIsOpen: boolean = false;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchEvents();
  }

  fetchEvents(): void {
    const getStart: any = {
      month: startOfMonth,
      week: startOfWeek,
      day: startOfDay
    }[this.view];

    const getEnd: any = {
      month: endOfMonth,
      week: endOfWeek,
      day: endOfDay
    }[this.view];

    const params = new HttpParams()
      .set(
        'primary_release_date.gte',
        format(getStart(this.viewDate), 'YYYY-MM-DD')
      )
      .set(
        'primary_release_date.lte',
        format(getEnd(this.viewDate), 'YYYY-MM-DD')
      )
      .set('api_key', '0ec33936a68018857d727958dca1424f');

    this.events$ = this.http
      .get('https://api.themoviedb.org/3/discover/movie', { params })
      .pipe(
        map(({ results }: { results: Release[] }) => {
          return results.map((release: Release) => {
            return {
              title: release.title,
              start: new Date(release.release_date),
              color: colors.yellow,
              meta: {
                release
              }
            };
          });
        })
      );
  }

  dayClicked({
    date,
    events
  }: {
    date: Date;
    events: Array<CalendarEvent<{ release: Release }>>;
  }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
        this.viewDate = date;
      }
    }
  }

  eventClicked(event: CalendarEvent<{ release: Release }>): void {
    window.open(
      `https://www.themoviedb.org/movie/${event.meta.release.id}`,
      '_blank'
    );
  }


  musicEvents(): void {
    this.events2$ = this.http
      .get('http://localhost:63613/music/page/1')
      .pipe(
        map(({ results }: { results: ReleaseMusic[] }) => {
          return results.map((release: ReleaseMusic) => {
            return {
              title: '123',
              start: new Date(2018, 6, 3),
              color: colors.yellow,
              meta: {
                release
              }
            };
          });
        })
      );
  }
}
