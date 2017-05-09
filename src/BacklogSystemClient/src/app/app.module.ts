import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { BacklogComponent } from './backlog/backlog.component';
import { PbiComponent } from './pbi/pbi.component';

@NgModule({
  declarations: [
    AppComponent,
    BacklogComponent,
    PbiComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
