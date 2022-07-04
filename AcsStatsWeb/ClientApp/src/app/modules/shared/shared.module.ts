import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RecordsHeaderComponent} from "./components/records-header/records-header.component";
import {RouterModule} from "@angular/router";


@NgModule({
  declarations: [RecordsHeaderComponent],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [RecordsHeaderComponent]
})
export class SharedModule {
}
