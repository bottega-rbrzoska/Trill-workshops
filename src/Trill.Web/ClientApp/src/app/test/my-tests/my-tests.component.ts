import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { BehaviorSubject, observable, Observable, of, Subject } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { TestData } from '../../models/TestData';
import { TestService } from '../test.service';

@Component({
  selector: 'app-my-tests',
  templateUrl: './my-tests.component.html',
  styleUrls: ['./my-tests.component.scss']
})
export class MyTestsComponent implements OnInit {


  // obs$ = new Observable(obs => {
  //   obs.next(1);
  //   obs.error('oooooh f****ck');
  //   obs.next(2);
  //   obs.next(3);
  //   obs.complete();
  // });
  obs$ = of(1,2,3,4,5)

  mySubj = new Subject();
  myBSubj = new BehaviorSubject('a');

  counter;
  isOnline = false;
  testData: TestData = {
    testAge: 18,
    testName: 'MyName'
  };

  testCol: TestData[] = [
    {
      testAge: 22,
      testName: 'MyName1'
    }, {
      testAge: 33,
      testName: 'MyName2'
    }
  ];
  constructor(private testService: TestService) {
    this.counter = this.testService.counter;
   }

  ngOnInit(): void {
    this.myBSubj.pipe(
      tap(v => console.log(v)),
      map(v => v.toUpperCase()))
      .subscribe( value => console.log(value), err=> console.log(err), () => console.log('Completed'));

    this.mySubj.next(1)
  }

  handleChildClick(name) {
    console.log(name);
  }

  toggleOnline() {
    this.testService.incr();
    this.counter = this.testService.counter;
    this.isOnline = !this.isOnline;
  }

}
