import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { ChatListComponent } from './chat-list/chat-list.component';
import { RegisterComponent } from './home/register/register.component';
import { LoginComponent } from './home/login/login.component';
import { ChatListElementComponent } from './chat-list/chat-list-element/chat-list-element.component';

//Material
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatRippleModule } from '@angular/material/core';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    ChatListComponent,
    ChatListElementComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatCardModule,
    MatTabsModule,
    MatInputModule,
    MatButtonModule,
    MatDividerModule,
    MatRippleModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
