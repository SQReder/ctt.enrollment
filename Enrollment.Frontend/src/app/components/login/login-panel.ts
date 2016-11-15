import {Component} from "@angular/core";
import {AuthService} from "../../services/auth/auth-service";

@Component({
  selector: "[ctt-login-panel]",
  templateUrl: "login-panel.html"
})
export class LoginPanel {
  constructor(
    private authService: AuthService
  ) {
  }

  logout(): Promise<any> {
    return this.authService.logout();
  }
}
