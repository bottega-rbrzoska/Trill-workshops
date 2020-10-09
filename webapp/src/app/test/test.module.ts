import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TestRoutingModule } from './test-routing.module';
import { MyTestsComponent } from './my-tests/my-tests.component';

export const testData = 'sdsdsdsd';

@NgModule({
  declarations: [MyTestsComponent],
  imports: [
    CommonModule,
    TestRoutingModule
  ]
})
export class TestModule { }
