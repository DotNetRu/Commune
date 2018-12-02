import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit } from "@angular/core";
import { MatDrawer, MatSnackBar, MatSnackBarConfig } from "@angular/material";
import { IMessage, LayoutService } from "@dotnetru/core";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: "mtp-toolbar",
    styleUrls: ["./toolbar.component.css"],
    templateUrl: "./toolbar.component.html",
})
export class ToolbarComponent implements OnInit {
    @Input() public drawer?: MatDrawer;

    public percentage: number = 0;
    public showProgressBar: boolean = false;

    constructor(
        private _snackBar: MatSnackBar,
        private _layoutService: LayoutService,
        private _changeDetectorRef: ChangeDetectorRef,
    ) { }

    public getProgressMode = (): string => {
        return !this.percentage
            ? "indeterminate"
            : (this.percentage < 100 ? "determinate" : "query");
    }

    public ngOnInit(): void {
        this._layoutService.messages$.subscribe((message: IMessage) => {
            const snackBarConfig: MatSnackBarConfig<IMessage> = {
                direction: "ltr",
                duration: message.duration,
                horizontalPosition: "center",
                panelClass: `snack-bar-${message.severity}`,
                politeness: "assertive",
                verticalPosition: "top",
            };
            this._snackBar.open(message.text, "Закрыть", snackBarConfig);
        });

        this._layoutService.progress$.subscribe((progress: boolean | number) => {
            this.showProgressBar = !!progress;
            this.percentage = this.showProgressBar && typeof progress === "number" ? progress : 0;
            this._changeDetectorRef.detectChanges();
        });
    }
}
