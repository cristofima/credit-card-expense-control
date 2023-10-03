import { Injectable } from '@angular/core';
import { ReportUtil } from '../utils/report.util';
import { ReportModel } from '../models/report.model';
import { CreditCardModel } from '../models/credit-card.model';
import { HttpClient } from '@angular/common/http';
import { ReportTransactionModel } from '../models/transaction.model';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient) { }

  private baseApiUrl = 'http://localhost:5021/api';

  getTransactionsReportByYear(year: number) {
    return this.http.get<ReportModel[]>(`${this.baseApiUrl}/Reports/${year}`);
  }

  async getReportTransactions(transactions: ReportTransactionModel[], creditCard: CreditCardModel, month: number, year: number) {
    const ts = transactions.filter(transaction => transaction.creditCard.id == creditCard.id);
    return ReportUtil.getTransacctionsToBePaid(ts, creditCard.cutOffDay, month, year);
  }

}
