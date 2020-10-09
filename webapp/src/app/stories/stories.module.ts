import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StoriesRoutingModule } from './stories-routing.module';
import { StoriesListComponent } from './stories-list/stories-list.component';


@NgModule({
  declarations: [StoriesListComponent],
  imports: [
    CommonModule,
    StoriesRoutingModule
  ],
  providers: []
})
export class StoriesModule { }
