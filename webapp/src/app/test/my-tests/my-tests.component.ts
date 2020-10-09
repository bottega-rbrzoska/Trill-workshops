import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TestData } from '../../models/TestData';
import { TestService } from '../test.service';

@Component({
  selector: 'app-my-tests',
  templateUrl: './my-tests.component.html',
  styleUrls: ['./my-tests.component.scss']
})
export class MyTestsComponent implements OnInit {

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
