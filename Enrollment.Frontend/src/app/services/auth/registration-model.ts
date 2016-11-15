export class RegistrationModel {
  username: string;
  password: string;

  get isPasswordValid(): boolean {
    const isValid = this.passwordHasDigits && this.passwordHasLetter && this.passwordLongEnough;
    return isValid;
  }

  get passwordHasDigits(): boolean {
    const hasDigits = this.password != null && this.password.match(/\d+/) != null;
    return hasDigits;
  }

  get passwordHasLetter(): boolean {
    const hasLetter = this.password != null && this.password.match(/[A-Z]+/i) != null;
    return hasLetter;
  }

  get passwordLongEnough(): boolean {
    const isLongEnough = this.password != null && this.password.length >= 8;
    return isLongEnough;
  }

  get isUsernameLongEnough(): boolean {
    const isLongEnough = this.username != null && this.username.length > 4;
    return isLongEnough;
  }

  get isUsernameValid(): boolean {
    const isValid = this.isUsernameLongEnough;
    return isValid;
  }

  get isModelValid(): boolean {
    return this.isPasswordValid && this.isUsernameValid;
  }
}
