import { Component, OnInit, Output, EventEmitter, Input, AfterViewChecked } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-paging',
  templateUrl: './paging.component.html',
  styleUrls: ['./paging.component.css']
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

  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    const pageParam = +this.route.snapshot.queryParamMap.get('page');
    this.setPage(pageParam > 0 ? pageParam : 1);
  }

  setPage(page: number) {
    this.page = page;
    this.setDisplayPages();
    this.router.navigate([], { queryParams: { page: this.page } });
    this.pageChanged.emit(this.page);
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
