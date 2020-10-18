import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StoriesRoutingModule } from './stories-routing.module';
import { StoriesListComponent } from './stories-list/stories-list.component';
import { StoriesService } from './stories.service';
import { StoriesAddComponent } from './stories-add/stories-add.component';
import { StoriesDetailsComponent } from './stories-details/stories-details.component';


@NgModule({
  declarations: [StoriesListComponent, StoriesAddComponent, StoriesDetailsComponent],
  imports: [
    CommonModule,
    StoriesRoutingModule
  ],
  providers: [StoriesService]
})
export class StoriesModule { }
