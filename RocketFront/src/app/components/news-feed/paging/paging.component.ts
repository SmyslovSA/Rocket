import { Component, OnInit, Output, EventEmitter, Input, AfterViewChecked } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-paging',
  templateUrl: './paging.component.html',
  styleUrls: ['./paging.component.css'],
  providers: [Location]
})
export class PagingComponent implements OnInit, AfterViewChecked {

  @Output() pageChanged = new EventEmitter<number>();
  page: number;
  displayPages: number[] = [];

  private _pageCount: number;
  public get pageCount(): number {
    return this._pageCount;
  }
  @Input() public set pageCount(v: number) {
    this._pageCount = v;
    this.setDisplayPages();
  }

  constructor(private route: ActivatedRoute, private router: Router, private location: Location) { }

  ngOnInit() {
    this.route.queryParamMap.subscribe(params =>
      this.onPageParamChanged(+params.get('page')));
  }

  onPageParamChanged(page: number) {
    if (page < 1) {
      page = 1;
      this.location.replaceState(this.location.path(), 'page=1');
    }
    this.page = page;
    this.setDisplayPages();
    this.pageChanged.emit(this.page);
  }

  setPage(page: number) {
    this.router.navigate([], { queryParams: { page: page } });
  }

  setDisplayPages() {
    const displayCount = 5;
    this.displayPages = [];
    if (this.pageCount > 0) {
      this.displayPages.push(this.page);
      for (let index = 1; index <= displayCount - 1; index++) {
        if (this.page - index > 0) {
          this.displayPages.push(this.page - index);
        }
        if (this.page + index <= this.pageCount) {
          this.displayPages.push(this.page + index);
        }
        if (this.displayPages.length >= displayCount) {
          this.displayPages.sort((a, b) => a - b);
          break;
        }
      }
    }
  }

  ngAfterViewChecked(): void {
    window.scrollTo(0, 0);
    }

}
