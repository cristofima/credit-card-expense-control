import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreditCardsComponent } from './components/credit-cards/credit-cards.component';
import { TransactionsComponent } from './components/transactions/transactions.component';
import { ReportsComponent } from './components/reports/reports.component';

const routes: Routes = [
  {
    path: 'credit-cards',
    component: CreditCardsComponent,
  },
  {
    path: 'transactions',
    component: TransactionsComponent,
  },
  {
    path: 'reports',
    component: ReportsComponent,
  },
  {
    path: '',
    redirectTo: 'credit-cards',
    pathMatch: 'full'
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
