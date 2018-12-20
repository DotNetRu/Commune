import { ChangeDetectionStrategy, Component, Input } from "@angular/core";
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from "@angular/forms";

import { CITIES } from "./constants";
import { City } from "./enums";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [
        { provide: NG_VALUE_ACCESSOR, useExisting: CitySelectComponent, multi: true },
    ],
    selector: "mtp-city-select",
    styleUrls: ["./city-select.component.css"],
    templateUrl: "./city-select.component.html",
})
export class CitySelectComponent implements ControlValueAccessor {
    public CITIES: City[] = CITIES;

    @Input()
    public set readonly(newValue: boolean) {
        if (newValue === true) {
            this.CITIES = [this._value];
        } else {
            this.CITIES = CITIES;
        }
    }

    @Input()
    public get value(): City { return this._value; }
    public set value(newValue: City) {
        if (newValue && newValue !== this._value) {
            this._value = newValue;
            this._changed.forEach((f) => f(newValue));
        }
    }

    private _value: City = City.Spb;
    private _changed = new Array<(value: City) => void>();
    private _touched = new Array<() => void>();

    public touch() {
        this._touched.forEach((f) => f());
    }

    public writeValue(newValue: City): void {
        this._value = newValue;
    }

    public registerOnChange(fn: any): void {
        this._changed.push(fn);
    }

    public registerOnTouched(fn: any): void {
        this._touched.push(fn);
    }
}
