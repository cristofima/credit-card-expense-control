import { CreditCardService } from './credit-card.service';
import { Injectable } from '@angular/core';
import { TransactionService } from './transaction.service';
import { ReportUtil } from '../utils/report.util';
import { ReportModel } from '../models/report.model';
import { CreditCardModel } from '../models/credit-card.model';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private creditCardService: CreditCardService, private transactionService: TransactionService) { }

  getTransactionsReportByYear(year: number) {
    let report: ReportModel[] = [];
    let creditCards = this.creditCardService.getCreditCards();
    const months = Array.from({ length: 12 }, (_, index) => index + 1);

    creditCards.forEach(creditCard => {
      let rm: ReportModel = {
        creditCardId: creditCard.id,
        creditCardName: creditCard.name,
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

      months.forEach(month => {
        let ts = this.getReportTransactions(creditCard, month, year);
        let total = ts.reduce((total, transaction) => total + transaction.amount / transaction.quotas, 0);
        if (month == 1) rm.january = total;
        else if (month == 2) rm.february = total;
        else if (month == 3) rm.march = total;
        else if (month == 4) rm.april = total;
        else if (month == 5) rm.may = total;
        else if (month == 6) rm.june = total;
        else if (month == 7) rm.july = total;
        else if (month == 8) rm.august = total;
        else if (month == 9) rm.september = total;
        else if (month == 10) rm.october = total;
        else if (month == 11) rm.november = total;
        else if (month == 12) rm.december = total;
      });

      report.push(rm);
    });

    return report;
  }

  getReportTransactions(creditCard: CreditCardModel, month: number, year: number) {
    let transactions = this.transactionService.getReportTransactions();
    const ts = transactions.filter(transaction => transaction.creditCard.id == creditCard.id);
    return ReportUtil.getTransacctionsToBePaid(ts, creditCard.cutOffDate, month, year);
  }

}
