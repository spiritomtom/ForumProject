import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { CommentComponent } from './comments/comment/comment.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { ModeratorPanelComponent } from './moderator/moderator-panel/moderator-panel.component';

const routes: Routes = [
  { path: '', component: CommentComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'admin', component: AdminDashboardComponent },
  { path: 'moderator', component: ModeratorPanelComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
