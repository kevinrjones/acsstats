import {Component, Input, OnInit} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {Observable} from "rxjs";
import {MatchSubTypeModel} from "../../../../models/match-sub-type.model";

@Component({
  selector: 'app-search-header',
  templateUrl: './records-search-select.component.html',
  styleUrls: ['./records-search-select.component.css']
})
export class RecordsSearchSelectComponent implements OnInit {

  @Input() parentFormGroup!: FormGroup
  @Input() matchSubTypes$!: Observable<MatchSubTypeModel[]>

  ngOnInit(): void {
  }

}
