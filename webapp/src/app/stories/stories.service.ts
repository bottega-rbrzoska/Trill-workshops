import { Injectable } from '@angular/core';
import { Story } from '../models/Story';

@Injectable()
export class StoriesService {

  private stories: Story[] = [
    { id: '1', author: 'Alojzy Jeż', createdAt: new Date(), tags: [], title: 'Super story'},
    { id: '2', author: 'Jan Kowalski', createdAt: new Date(), tags: [], title: 'Kowalski Super story'},
    { id: '3', author: 'Tomasz Bebok', createdAt: new Date(), tags: [], title: ' Tomasz Bebok story'}
  ];
  constructor() { }

  getStories() {
    return this.stories;
  }
}
