import { Component, Input, Output, EventEmitter } from "@angular/core"

@Component({
    selector: "app-debugger-toolbox",
    templateUrl: "./code-debugger-toolbox.component.html"
})

export class CodeDebuggerToolBoxComponent {

    @Input() state = "Debugging";
    @Output() action: EventEmitter<any> = new EventEmitter();

    Rodar() {
        this.action.emit("Rodar");
    }
    Proximo() {
        this.action.emit("Proximo");
    }
    Parar() {
        this.action.emit("Parar");
    }
    Direto() {
        this.action.emit("Direto");
    }
}