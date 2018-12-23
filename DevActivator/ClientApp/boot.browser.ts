import "reflect-metadata";
import "zone.js";
// tslint:disable-next-line:ordered-imports
import { enableProdMode, NgModuleRef } from "@angular/core";
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";
import { AppModule } from "./app/app.browser.module";

if (module.hot) {
    module.hot.accept();
    module.hot.dispose(() => {
        // Before restarting the app, we create a new root element and dispose the old one
        const oldRootElem = document.querySelector("app");
        const newRootElem = document.createElement("app");
        oldRootElem!.parentNode!.insertBefore(newRootElem, oldRootElem);
        modulePromise.then((appModule: NgModuleRef<AppModule>) => {
            appModule.destroy();
            oldRootElem!.innerHTML = "";
            const elements: HTMLCollectionOf<Element> = document.getElementsByClassName("cdk-overlay-container");
            // tslint:disable-next-line:prefer-for-of
            for (let i = 0; i < elements.length; i++) {
                elements[i].innerHTML = "";
            }
        });
    });
} else {
    enableProdMode();
}

// Note: @ng-tools/webpack looks for the following expression when performing production
// builds. Don't change how this line looks, otherwise you may break tree-shaking.
const modulePromise = platformBrowserDynamic().bootstrapModule(AppModule);
