import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TestRoutingModule } from './test-routing.module';
import { MyTestsComponent } from './my-tests/my-tests.component';
import { ChildTestComponent } from './child-test/child-test.component';

export const testData = 'sdsdsdsd';

@NgModule({
  declarations: [MyTestsComponent, ChildTestComponent],
  imports: [
    CommonModule,
    TestRoutingModule
  ]
})
export class TestModule { }
