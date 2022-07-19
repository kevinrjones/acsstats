import {Component, OnInit} from '@angular/core';
import {createSelector, Store} from "@ngrx/store";
import {AppState} from "./models/app-state";
import {ErrorDetails} from "./models/error.model";
import {SetErrorState} from "./actions/error.actions";
import {Observable} from "rxjs";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  // selectFeature = (state: AppState) => state.errorState;
  //
  // selectError = createSelector(
  //   this.selectFeature,
  //   (state: ErrorDetails) => state
  // );
  private errorState$: Observable<ErrorDetails>;

  constructor(private appStore: Store<AppState>, private toastr: ToastrService) {

    this.resetErrorState()

    this.errorState$ = this.appStore.select(s => s.errorState);
  }

  ngOnInit(): void {
    this.errorState$.subscribe(s => {
        console.log("App Store")
        if (s.id != 0) {
          this.toastr.error("Error", "Error Title")
          console.log("AppStore: error", s.id)
          this.resetErrorState()
        }
      }
    );
  }

  resetErrorState() {
    this.appStore.dispatch(SetErrorState({payload: {id: 0, message: null}}))
  }
}
