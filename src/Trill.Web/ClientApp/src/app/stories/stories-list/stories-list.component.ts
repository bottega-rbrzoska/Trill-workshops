import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { share } from 'rxjs/operators';
import { Story } from 'src/app/models/Story';
import { StoriesService } from '../stories.service';

@Component({
  selector: 'app-stories-list',
  templateUrl: './stories-list.component.html',
  styleUrls: ['./stories-list.component.scss']
})
export class StoriesListComponent implements OnInit, OnDestroy {

  subscription: Subscription;
  stories$: Observable<Story[]>;
  storiesCount$: Observable<number>;
  constructor(private storiesService: StoriesService) {
    this.stories$ = this.storiesService.stories$;
    this.storiesCount$ = this.storiesService.storiesCount$;

    this.subscription = this.storiesCount$.subscribe(v => console.log(v));
    this.storiesService.fetchStories();
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
