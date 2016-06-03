import {Directive, ViewContainerRef, TemplateRef} from 'angular2/core';
import {AuthService} from "./auth.service"

@Directive({
    selector: '[isNotLoggedIn]'
})

export class IsNotLoggedInDirective {

    constructor(
        private viewContainer: ViewContainerRef,
        private templateRef: TemplateRef,
        private authService: AuthService
    ) {

        this.updateVisibility(!authService.check());
        authService.onIsLoggedInChanged.subscribe((isLoggedIn) => this.onIsLoggedInChangedHandler(isLoggedIn));
    }

    private onIsLoggedInChangedHandler(isLoggedIn: boolean) {
        this.updateVisibility(!isLoggedIn);
    }

    private updateVisibility(visible: boolean) {
        if (visible) {
            this.viewContainer.createEmbeddedView(this.templateRef);
        } else {
            this.viewContainer.clear();
        }
    }
}