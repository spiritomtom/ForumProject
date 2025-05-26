import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';  // Your routing module
import { AppComponent } from './app.component';

// Auth Components
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';

// Comments Component
import { CommentComponent } from './comments/comment/comment.component';

// Admin Components
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';

// Moderator Components
import { ModeratorPanelComponent } from './moderator/moderator-panel/moderator-panel.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    CommentComponent,
    AdminDashboardComponent,
    ModeratorPanelComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,         // For HTTP calls to your API
    FormsModule,              // For Template-driven forms
    ReactiveFormsModule       // For Reactive forms (if you use them)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
