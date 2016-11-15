import {Directive, TemplateRef, ViewContainerRef} from "@angular/core";
import {AuthService} from "./auth-service";

@Directive({
  selector: "[whenLoggedIn]",
  providers: []
})
export class WhenLoggedInDirective {
  private viewCreated: boolean;

  constructor(
    private _viewContainer: ViewContainerRef,
    private _templateRef: TemplateRef<WhenLoggedInDirective>,
    private authService: AuthService
  ) {
    this.updateVisibility(this.authService.isLoggedIn);
    this.authService.isLoggedInChanged.subscribe(this.onIsLoggedInChangedHandler);
  }

  onIsLoggedInChangedHandler = (isLoggedIn: boolean) => {
    this.updateVisibility(isLoggedIn);
  };

  updateVisibility(isLoggedIn: boolean) {
    if (isLoggedIn) {
      if (this.viewCreated != true) {
        this._viewContainer.createEmbeddedView(this._templateRef);
        this.viewCreated = true;
      }
    } else {
      this._viewContainer.clear();
      this.viewCreated = false;
    }
  }
}
