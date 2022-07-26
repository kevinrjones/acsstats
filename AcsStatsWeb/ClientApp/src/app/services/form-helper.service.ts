import {Injectable} from "@angular/core";

@Injectable({providedIn: 'root'})
export class FormHelperService {

   private readonly defaultMatchType = "itt"

  getDefaultMatchType() {
    return this.defaultMatchType;
  }

  isNotFirstClass(matchType: string) {
    return !this.isFirstClass(matchType)
  }

  isFirstClass(matchType: string) {
    return (matchType === 't' || matchType == 'f' || matchType == 'wt' || matchType == 'wf');
  }

  isNotSeries(matchType: string) {

    return !(matchType === 't' || matchType == 'itt' || matchType == 'o' || matchType == 'witt' || matchType == 'wt' || matchType == 'wo');
  }

}
