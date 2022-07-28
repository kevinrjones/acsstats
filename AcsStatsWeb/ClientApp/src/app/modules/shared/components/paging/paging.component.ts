import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {faAngleLeft, faAngleRight, faAnglesLeft, faAnglesRight, faCoffee, faW} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-paging',
  templateUrl: './paging.component.html',
  styleUrls: ['./paging.component.css']
})
export class PagingComponent implements OnInit, OnChanges {

  @Input() pageSize!: number
  @Input() totalItems!: number
  @Input() currentPage!: number
  @Input() showGotoPage!: boolean;

  totalPages!: number
  currentFirstItem!: number;
  currentLastItem!: number;

  @Output() first: EventEmitter<void> = new EventEmitter();
  @Output() previous: EventEmitter<number> = new EventEmitter();
  @Output() next: EventEmitter<number> = new EventEmitter();
  @Output() last: EventEmitter<number> = new EventEmitter();
  @Output() goto: EventEmitter<number> = new EventEmitter();
  gotoPageNumber: number;

  faAngleRight = faAngleRight;
  faAnglesRight = faAnglesRight;
  faAngleLeft = faAngleLeft;
  faAnglesLeft = faAnglesLeft;

  constructor() {
    this.gotoPageNumber = 1
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.calculatePagingValues();
  }

  ngOnInit(): void {
    this.calculatePagingValues();
  }

  private calculatePagingValues() {
    this.currentFirstItem = 1 + (this.pageSize * (this.currentPage - 1));
    this.currentLastItem = (this.totalItems < this.pageSize ? this.totalItems : this.pageSize + this.currentFirstItem);
    if (this.currentLastItem > this.totalItems) this.currentLastItem = this.totalItems;
    let extrapage = this.totalItems % this.pageSize == 0 ? 0 : 1
    this.totalPages = Math.floor((this.totalItems / this.pageSize) + extrapage)
  }

  clickFirst() {
    this.first.emit()
  }

  clickPrevious() {
    let startRow = this.currentFirstItem - this.pageSize - 1

    if (startRow < 0) startRow = 0

    this.previous.emit(startRow)
  }

  clickNext() {
    let startRow = this.currentFirstItem + this.pageSize - 1
    this.next.emit(startRow)
  }

  clickLast() {
    let totalPages = Math.floor(this.totalItems / this.pageSize)

    let startRow = totalPages * this.pageSize

    this.last.emit(startRow)
  }

  clickGoto() {

    this.currentPage = this.gotoPageNumber;
    let startRow = (this.currentPage - 1) * this.pageSize

    while (startRow > this.totalItems) {
      this.currentPage--;
      startRow = (this.currentPage - 1) * this.pageSize
    }


    this.goto.emit(startRow)
  }

  valueChange($event: Event) {
    console.log("event", $event)
  }

  shouldShowGotoControl() {
    return this.showGotoPage ? "" : "hidePage"
  }

  isNavRightDisabled() {
    if(this.currentPage === this.totalPages) {
      return "item-disabled"
    }
    return ""
  }

  isNavLeftDisabled() {
    if(this.currentFirstItem === 1) {
      return "item-disabled"
    }
    return ""
  }
}
