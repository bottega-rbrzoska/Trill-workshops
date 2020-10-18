import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Story } from '../models/Story';

@Injectable()
export class StoriesService {

  private storiesSubj = new BehaviorSubject<Story[]>(null)
  storiesCount$ = this.storiesSubj.pipe(map(stories => stories ? stories.length : 0));
  stories$ = this.storiesSubj.asObservable();

  constructor(private http: HttpClient) { }

  fetchStories(): void {
    this.http.get<Story[]>('http://localhost:5000/api/stories').subscribe(stories => this.storiesSubj.next(stories));
  }
}
