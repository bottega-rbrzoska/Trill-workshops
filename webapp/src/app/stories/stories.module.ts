import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StoriesRoutingModule } from './stories-routing.module';
import { StoriesListComponent } from './stories-list/stories-list.component';
import { StoriesService } from './stories.service';


@NgModule({
  declarations: [StoriesListComponent],
  imports: [
    CommonModule,
    StoriesRoutingModule
  ],
  providers: [StoriesService]
})
export class StoriesModule { }
