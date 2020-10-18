import { Injectable } from '@angular/core';

@Injectable()
export class TestService {
  counter = 0;
  constructor() {
    console.log('init test service');
  }

  incr(): void {
    this.counter++;
  }
}
