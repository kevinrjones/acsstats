import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Observable} from "rxjs";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";

@Component({
  selector: 'app-records-header',
  templateUrl: './records-main-nav.component.html',
  styleUrls: ['./records-main-nav.component.css']
})
export class RecordsMainNavComponent implements OnInit {

  @Input() recordSummary$!: Observable<RecordsSummaryModel>;
  @Input() venue!: string;
  @Input() pageSize!: number
  @Input() totalItems!: number
  @Input() currentPage!: number

  @Input() returnUrl!: string;

  @Output() first: EventEmitter<void> = new EventEmitter();
  @Output() previous: EventEmitter<number> = new EventEmitter();
  @Output() next: EventEmitter<number> = new EventEmitter();
  @Output() last: EventEmitter<number> = new EventEmitter();
  @Output() goto: EventEmitter<number> = new EventEmitter();


  constructor() {
  }

  ngOnInit(): void {
  }


  raiseFirst() {
    this.first.emit()
  }

  raiseGoto($event: number) {
    this.goto.emit($event)
  }

  raisePrevious($event: number) {
    this.previous.emit($event)
  }

  raiseNext($event: number) {
    this.next.emit($event)
  }

  raiseLast($event: number) {
    this.last.emit($event)
  }
}
