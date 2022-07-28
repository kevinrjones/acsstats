import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from "@angular/router";
import {PagingComponent} from "./components/paging/paging.component";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {PopoverModule, TooltipModule} from "@coreui/angular";
import {RecordSummaryComponent} from "./components/record-summary/batting-summary.component";
import {RecordsSearchNavComponent} from "./components/records-search-nav/records-search-nav.component";
import {RecordsSearchSelectComponent} from "./components/search-select/records-search-select.component";
import {RecordsMainNavComponent} from "./components/records-main-nav/records-main-nav.component";
import {RecordsMainFooterComponent} from "./components/records-main-footer/records-main-footer.component";


@NgModule({
  declarations: [RecordSummaryComponent,
    RecordsSearchNavComponent,
    PagingComponent,
    RecordsSearchSelectComponent,
    RecordsMainNavComponent,
    RecordsMainFooterComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    FontAwesomeModule,
    PopoverModule,
    TooltipModule
  ],
  exports: [
    RecordsMainNavComponent,
    RecordsMainFooterComponent,
    RecordSummaryComponent,
    RecordsSearchNavComponent,
    PagingComponent,
    RecordsSearchSelectComponent,
    PopoverModule,
    TooltipModule
  ]
})
export class SharedModule {
}
