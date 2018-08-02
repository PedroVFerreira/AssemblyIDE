import { Component, Input } from "@angular/core"

@Component({
    selector: "app-debugger-viwer",
    templateUrl: "./code-debugger-viwer.component.html"
})

export class CodeDebuggerViwerComponent {

    @Input() code: string[] = [];
    @Input() currentLine: number = -1;

}