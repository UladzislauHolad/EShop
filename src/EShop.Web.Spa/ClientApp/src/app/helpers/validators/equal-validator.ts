import { Validator, ValidationErrors, AbstractControl, NG_VALIDATORS } from "@angular/forms";
import { Directive, Input } from "@angular/core";
import { Subscription } from "rxjs";

@Directive({
    selector: '[equal]',
    providers: [{ provide: NG_VALIDATORS, useExisting: EqualValidator, multi: true }]
})
export class EqualValidator implements Validator {
    @Input('equal') controlNameToCompare: string;

    validate(control: AbstractControl): ValidationErrors | null {
        const controlToCompare = control.root.get(this.controlNameToCompare);
        if (controlToCompare) {
            const subscription: Subscription = controlToCompare.valueChanges.subscribe(
                () => {
                    control.updateValueAndValidity();
                    subscription.unsubscribe();
                }
            );
        }

        return controlToCompare && controlToCompare.value !== control.value ? { 'equal': true } : null;
    }
}