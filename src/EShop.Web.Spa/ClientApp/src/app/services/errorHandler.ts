import { HttpErrorResponse } from "@angular/common/http/index";
import { throwError } from "rxjs";

export class errorHandler {
    public handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
          // A client-side or network error occurred. Handle it accordingly.
          console.error('An error occurred:', error.error.message);
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong,
          console.error(
            `Backend returned code ${error.status}, ` +
            `body was: ${error.error}`);
        }

        let message = 'Something bad happened! Please try again later.';

        if(error.error.message)
          message = error.error.message;
        // return an observable with a user-facing error message
        return throwError(message);
      };
}