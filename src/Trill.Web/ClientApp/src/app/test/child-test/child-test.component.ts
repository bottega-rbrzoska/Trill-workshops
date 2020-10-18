import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TestData } from 'src/app/models/TestData';

@Component({
  selector: 'app-child-test',
  templateUrl: './child-test.component.html',
  styleUrls: ['./child-test.component.scss']
})
export class ChildTestComponent implements OnInit {

  @Output() childButtonClick = new EventEmitter<string>();
  @Input() childData: TestData;
  constructor() {
    console.log('constructor: ', this.childData)
  }

  ngOnInit(): void {

    console.log('onInit: ', this.childData)
  }


  handleClick() {
    this.childButtonClick.emit(this.childData.testName.toUpperCase())
  }

}
