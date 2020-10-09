import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MyTestsComponent } from './my-tests/my-tests.component';

const routes: Routes = [
  { path: '', component: MyTestsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TestRoutingModule { }
