import {Component, Input, OnInit} from '@angular/core';
import {Observable} from "rxjs";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";

@Component({
  selector: 'app-records-summary',
  templateUrl: './batting-summary.component.html',
  styleUrls: ['./batting-summary.component.css']
})
export class RecordSummaryComponent implements OnInit {

  @Input()
  recordSummary$!: Observable<RecordsSummaryModel>;
  @Input()
  venue!: string;

  constructor() { }

  ngOnInit(): void {
  }

}
