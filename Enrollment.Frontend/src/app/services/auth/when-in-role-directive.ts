import {Directive, TemplateRef, ViewContainerRef} from "@angular/core";
import {AuthService} from "./auth-service";
import {Input} from "@angular/core";

@Directive({
  selector: "[whenInRole]",
  providers: []
})
export class WhenInRoleDirective {
  private viewCreated: boolean;

  private role: string;

  constructor(
    private _viewContainer: ViewContainerRef,
    private _templateRef: TemplateRef<WhenInRoleDirective>,
    private authService: AuthService
  ) {
    this.updateVisibility(this.authService.isLoggedIn, this.authService.roles);
    this.authService.isLoggedInChanged.subscribe(this.onIsLoggedInChangedHandler);
  }

  @Input() set whenInRole(role: string) {
    this.role = role;
    this.updateVisibility(this.authService.isLoggedIn, this.authService.roles);
  }

  onIsLoggedInChangedHandler = (isLoggedIn: boolean) => {
    this.updateVisibility(isLoggedIn, this.authService.roles);
  };

  updateVisibility(isLoggedIn: boolean, roles: string[]) {
    if (isLoggedIn && roles.find((val) => val === this.role)) {
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
