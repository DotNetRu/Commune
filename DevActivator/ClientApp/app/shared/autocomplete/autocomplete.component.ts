import { ChangeDetectionStrategy, Component, ElementRef, EventEmitter, Input, Output, ViewChild } from "@angular/core";
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs";
import { debounceTime, filter, map } from "rxjs/operators";

import { IAutocompleteRow } from "./interfaces";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: "mtp-autocomplete",
    templateUrl: "./autocomplete.component.html",
})
export class AutocompleteComponent {
    @Input() public title: string = "";
    @Input() public iconName: string = "";
    @Input() public iconText: string = "";
    @Input() public data: IAutocompleteRow[] = [];

    @Output() public readonly selected: EventEmitter<IAutocompleteRow> = new EventEmitter<IAutocompleteRow>();
    @Output() public readonly iconClicked: EventEmitter<void> = new EventEmitter<void>();

    public data$: Observable<IAutocompleteRow[]>;
    public queryControl: FormControl;

    constructor() {
        this.queryControl = new FormControl(undefined);
        this.data$ = this.queryControl.valueChanges
            .pipe(
                filter((val) => typeof val === "string"),
                debounceTime(300),
                map((query: string) => {
                    const searchPhrases: string[] = (query || "")
                        .toLowerCase()
                        .replace(/\s+/, " ")
                        .split(" ")
                        .map((x) => x.replace(/[^а-яёa-z\d]/gi, ""));
                    return this.data.filter((x) => {
                        const lowerName: string = x.name.toLowerCase();
                        let ix: number = 0;
                        for (const searchPhrase of searchPhrases) {
                            // important order
                            ix = lowerName/* .substring(ix) */.indexOf(searchPhrase);
                            if (ix < 0) {
                                return false;
                            }
                        }

                        return true;
                    });
                }),
            );
    }

    public onSelected(row: IAutocompleteRow): void {
        this.queryControl.patchValue(row.name);
        this.selected.emit(row);
    }

    public onIconClick(): void {
        this.iconClicked.emit();
    }
}
