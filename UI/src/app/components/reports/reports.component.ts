import { Component, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { CreditCardModel } from 'src/app/models/credit-card.model';
import { ReportModel } from 'src/app/models/report.model';
import { ReportTransactionModel } from 'src/app/models/transaction.model';
import { CreditCardService } from 'src/app/services/credit-card.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent implements OnInit {

  constructor(private creditCardService: CreditCardService, private reportService: ReportService) { }

  transactionsReport: ReportTransactionModel[] = [];
  report: ReportModel[] = [];
  summary!: ReportModel;
  private creditCards!: CreditCardModel[];

  year!: number;
  month!: number;
  stringMonth!: string;

  yearOptions: SelectItem[] = [];
  monthOptions: SelectItem[] = [];

  ngOnInit(): void {
    let today = new Date();
    this.year = today.getFullYear();
    this.month = today.getMonth() + 1;
    this.buildYearOptions();
    this.buildMonthOptions();

    this.creditCards = this.creditCardService.getCreditCards();
    this.onChangeYear();
  }

  private buildYearOptions() {
    for (let y = this.year - 1; y <= this.year + 2; y++) {
      this.yearOptions.push({ label: y.toString(), value: y });
    }
  }

  private buildMonthOptions() {
    const months = Array.from({ length: 12 }, (_, index) => index + 1);
    months.forEach(month => {
      let stringMonth = new Date(this.year, month - 1).toLocaleString('es-EC', { month: 'long' });
      this.monthOptions.push({ label: stringMonth, value: month });
    });
  }

  onChangeYear() {
    this.report = this.reportService.getTransactionsReportByYear(this.year);
    this.calculateTotalByMonth();
    this.onChangeMonth();
  }

  onChangeMonth() {
    this.stringMonth = new Date(this.year, this.month - 1).toLocaleString('es-EC', { month: 'long' });
    this.transactionsReport = [];
    this.creditCards.forEach(creditCard => {
      let arr = this.reportService.getReportTransactions(creditCard, this.month, this.year);
      this.transactionsReport = this.transactionsReport.concat(arr);
    });
  }

  caculateTotalByCreditCard(creditCardId: string) {
    let total = 0;
    this.transactionsReport.forEach(transaction => {
      if (transaction.creditCard.id == creditCardId) {
        total += transaction.amount / transaction.quotas;
      }
    });

    return total;
  }

  private calculateTotalByMonth() {
    this.summary = {
      january: 0,
      february: 0,
      march: 0,
      april: 0,
      may: 0,
      june: 0,
      july: 0,
      august: 0,
      september: 0,
      october: 0,
      november: 0,
      december: 0
    };

    const months = Array.from({ length: 12 }, (_, index) => index + 1);

    this.report.forEach(r => {
      months.forEach(month => {
        if (month == 1) this.summary.january += r.january;
        else if (month == 2) this.summary.february += r.february;
        else if (month == 3) this.summary.march += r.march;
        else if (month == 4) this.summary.april += r.april;
        else if (month == 5) this.summary.may += r.may;
        else if (month == 6) this.summary.june += r.june;
        else if (month == 7) this.summary.july += r.july;
        else if (month == 8) this.summary.august += r.august;
        else if (month == 9) this.summary.september += r.september;
        else if (month == 10) this.summary.october += r.october;
        else if (month == 11) this.summary.november += r.november;
        else if (month == 12) this.summary.december += r.december;
      });
    });
  }

}
