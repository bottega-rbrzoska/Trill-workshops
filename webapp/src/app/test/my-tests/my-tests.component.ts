import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TestData } from '../../models/TestData';

@Component({
  selector: 'app-my-tests',
  templateUrl: './my-tests.component.html',
  styleUrls: ['./my-tests.component.scss']
})
export class MyTestsComponent implements OnInit {

  testData: TestData = {
    testAge: 18,
    testName: 'MyName'
  };
  constructor() { }

  ngOnInit(): void {
  }
  handleChildClick(name) {
    console.log(name)
  }

}
