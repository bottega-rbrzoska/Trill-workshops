import { Component, OnInit } from '@angular/core';
import { Story } from 'src/app/models/Story';
import { StoriesService } from '../stories.service';

@Component({
  selector: 'app-stories-list',
  templateUrl: './stories-list.component.html',
  styleUrls: ['./stories-list.component.scss']
})
export class StoriesListComponent implements OnInit {

  stories: Story[];
  constructor(private storiesService: StoriesService) {
    this.stories = this.storiesService.getStories();
  }

  ngOnInit(): void {
  }

}
