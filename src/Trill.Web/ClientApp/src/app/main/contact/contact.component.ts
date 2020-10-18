import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

  myContactForm = new FormGroup({
    name: new FormControl(null, [Validators.required]),
    email: new FormControl(null, [Validators.email])
  }, { updateOn: 'blur'});

  constructor() { }

  ngOnInit(): void {
    this.myContactForm.valueChanges.subscribe(v => console.log(v))
  }

  handleSubmit() {
    console.log(this.myContactForm.value)
  }

}
