import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';

@Component({
  selector: 'app-paging',
  templateUrl: './paging.component.html',
  styleUrls: ['./paging.component.css']
})
export class PagingComponent implements OnInit, OnChanges {

  @Input() pageSize!: number
  @Input() totalItems!: number
  @Input() currentPage!: number

  totalPages!: number
  currentFirstItem!: number;
  currentLastItem!: number;
  currentItem!: number

  @Output() first: EventEmitter<void> = new EventEmitter();
  @Output() previous: EventEmitter<number> = new EventEmitter();
  @Output() next: EventEmitter<number> = new EventEmitter();
  @Output() last: EventEmitter<number> = new EventEmitter();
  @Output() goto: EventEmitter<number> = new EventEmitter();
  gotoPageNumber: number;

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
    this.currentLastItem = (this.totalItems < this.pageSize ? this.totalItems : this.pageSize + this.currentFirstItem) - 1;
    if(this.currentLastItem > this.totalItems) this.currentLastItem = this.totalItems;
    let extrapage = this.totalItems % this.pageSize == 0 ? 0 : 1
    this.totalPages =Math.floor((this.totalItems / this.pageSize) + extrapage)
  }

  clickFirst() {
    this.first.emit()
  }

  clickPrevious() {
    let startRow = this.currentFirstItem - this.pageSize - 1

    if(startRow < 0) startRow = 0

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

    while(startRow > this.totalItems) {
      this.currentPage--;
      startRow = (this.currentPage - 1) * this.pageSize
    }


    this.goto.emit(startRow)
  }

  valueChange($event: Event) {
    console.log("event", $event)
  }
}
