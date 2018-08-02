import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CodeDebuggerViwerComponent } from './components/code-debugger-viwer/code-debugger-viwer.component';
import { CodeDebuggerToolBoxComponent } from './components/code-debugger-toolbox/code-debugger-toolbox.component';

@NgModule({
    declarations: [
        AppComponent,
        FetchDataComponent,
        HomeComponent,
        CodeDebuggerViwerComponent,
        CodeDebuggerToolBoxComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
