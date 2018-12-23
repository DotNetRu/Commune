import { DebugElement } from "@angular/core";
import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { } from "jasmine";
import { AppComponent } from "./app.component";

describe("AppComponent", () => {
    let app: AppComponent;
    let de: DebugElement;
    let fixture: ComponentFixture<AppComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [
                AppComponent,
            ],
        });

        fixture = TestBed.createComponent(AppComponent);
        app = fixture.componentInstance;
        de = fixture.debugElement;
    }));

    it("should create the app", async(() => {
        expect(app).toBeDefined();
    }));

    it(`should have as title 'app'`, async(() => {
        expect(app.title).toEqual("app");
    }));
});
