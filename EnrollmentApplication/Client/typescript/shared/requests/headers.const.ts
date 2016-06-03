import { Headers } from 'angular2/http';

export const contentHeaders = new Headers();
contentHeaders.append('Content-Type', 'application/x-www-form-urlencoded');