import {OpaqueToken} from '@angular/core';

export const LocalStorage = new OpaqueToken('localStorage');

export const DummyLocalStorage = {
  getItem() {
  }
};
