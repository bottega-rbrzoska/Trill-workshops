import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StoriesAddComponent } from './stories-add/stories-add.component';
import { StoriesDetailsComponent } from './stories-details/stories-details.component';
import { StoriesListComponent } from './stories-list/stories-list.component';

const routes: Routes = [
  { path: '', component: StoriesListComponent },
  { path: 'add', component: StoriesAddComponent },
  { path: ':id', component: StoriesDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StoriesRoutingModule { }
