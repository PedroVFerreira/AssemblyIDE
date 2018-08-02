import { Component } from "@angular/core"

export class VirtualMachine {
    linhaAtual: number = -1;
    key: string = "";;
    codigo: string[] = [];
    log: string[] = [];
    registradores: Registradores = new Registradores();
    flags: Flags = new Flags();
    memoria: Dado[] = [];
}

class Registradores {
    ax: string  = "0000";
    bx : string = "0000";
    cx : string = "0000";
    dx : string = "0000";
    mar: string = "0000";
    mbr: string = "0000";
    pc : string = "0000";
}

class Flags {
    zeroFlag:     boolean = false;
    overflowFlag: boolean = false;
    signFlag    : boolean = false;
}

class Dado {
    endereco: number;
    valor: string;
    enderecoHex: string;
    valorHex: string;
}