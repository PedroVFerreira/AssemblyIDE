import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformServer, isPlatformBrowser } from '@angular/common';
import { Http } from '@angular/http';
import { VirtualMachine } from '../../class/Machine';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
//declare var localStorage:any;
export class HomeComponent {

    codeTxt: string = "";
    machine: VirtualMachine = new VirtualMachine();
    state = "Codding";
    erros = [];

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, @Inject(PLATFORM_ID) private platformId: Object) {
        if (isPlatformBrowser(this.platformId)) {
            let currentKey = localStorage.getItem("machine_key");
            if (currentKey != undefined && currentKey != null)
                this.loadMachine(currentKey);
        }
    }

    ToolboxAction(e:string) {
        if (e === "Rodar")
            this.submitCode();
        if (e === "Parar")
            this.limpar();
        if (e === "Proximo")
            this.proximo();
    }
    
    submitCode() {
        this.erros = [];
        this.state = "Loadding";
        this.http.post(this.baseUrl + 'api/VirtualMachine', this.codeTxt.split("\n"))
            .subscribe(result => {
                this.machine = result.json();
                localStorage.setItem("machine_key", this.machine.key);
                this.state = "Debugging";
            }, error => {
                console.error(error);
                this.state = "Codding";
                this.erros = error.json();
            });
    }

    loadMachine(key: string) {
        this.http.get(this.baseUrl + 'api/VirtualMachine/' + key)
            .subscribe(result => {
                this.machine = result.json();
                this.codeTxt = this.machine.codigo.join("\n");
                this.state = "Debugging";
            }, error => {
                this.limpar();
            });
    }

    limpar() {
        localStorage.removeItem("machine_key");
        this.state = "Codding";
    }

    proximo() {
        this.http.get(this.baseUrl + 'api/VirtualMachine/RunStep/' + this.machine.key)
            .subscribe(result => {
                this.machine = result.json();
            }, error => console.error(error));
        
    }
}
