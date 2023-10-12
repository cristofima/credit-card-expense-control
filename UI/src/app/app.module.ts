import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CreditCardsComponent } from './components/credit-cards/credit-cards.component';
import { TransactionsComponent } from './components/transactions/transactions.component';
import { ReportsComponent } from './components/reports/reports.component';

import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { ScrollTopModule } from 'primeng/scrolltop';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CardModule } from 'primeng/card';
import { TabMenuModule } from 'primeng/tabmenu';
import { CalendarModule } from 'primeng/calendar';
import { KeyFilterModule } from 'primeng/keyfilter';
import { RadioButtonModule } from 'primeng/radiobutton';
import { BadgeModule } from 'primeng/badge';
import { TooltipModule } from 'primeng/tooltip';
import { CheckboxModule } from 'primeng/checkbox';

@NgModule({
  declarations: [
    AppComponent,
    CreditCardsComponent,
    TransactionsComponent,
    ReportsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    InputNumberModule,
    DropdownModule,
    ButtonModule,
    TableModule,
    DialogModule,
    ConfirmDialogModule,
    ToastModule,
    ScrollTopModule,
    CardModule,
    TabMenuModule,
    CalendarModule,
    KeyFilterModule,
    RadioButtonModule,
    BadgeModule,
    TooltipModule,
    CheckboxModule
  ],
  providers: [ConfirmationService, MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
