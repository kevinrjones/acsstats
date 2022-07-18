import {Component, Input, OnInit} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {MatchSubTypeSearchService} from "../../../services/match-sub-type-search.service";
import {MatchSubTypeModel} from "../../../models/match-sub-type.model";
import {Observable} from "rxjs";

@Component({
  selector: 'app-search-header',
  templateUrl: './search-header.component.html',
  styleUrls: ['./search-header.component.css']
})
export class SearchHeaderComponent implements OnInit {

  @Input() parentFormGroup!: FormGroup
  @Input() matchSubTypes$!: Observable<MatchSubTypeModel[]>

  ngOnInit(): void {
  }

}
