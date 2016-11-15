import {Injectable} from "@angular/core";

@Injectable()
export class Cookie {
  get(name: string): string { return null; }

  protected readCookie(name: string, cookieProvider: any) {
    const nameEQ = name + "=";
    const ca: string[] = cookieProvider.split(";");
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) === " ") c = c.substring(1, c.length);
      if (c.indexOf(nameEQ) === 0)
        return c.substring(nameEQ.length, c.length);
    }
    return null;

  }
}

@Injectable()
export class CookieBrowser extends Cookie {
  get(name: string): string {
    return super.readCookie(name, window.document.cookie)
  }
}

@Injectable()
export class CookieNode extends Cookie {
  get(name: string): string {
    // ToDo implement using Zone.current.get('req').cookies
    return super.readCookie(name, "");
  }
}
