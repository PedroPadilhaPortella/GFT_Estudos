import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'app-message',
    templateUrl: './message.component.html',
    styles: [
    ]
})
export class MessageComponent {

    @Input() text = '';
}
