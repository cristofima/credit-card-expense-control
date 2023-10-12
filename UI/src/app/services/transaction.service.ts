import { Injectable } from '@angular/core';
import { ReportTransactionModel, TransactionModel } from '../models/transaction.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private http: HttpClient) { }

  private baseApiUrl = 'http://localhost:27972/api';


  getTransactions() {
    return this.http.get<TransactionModel[]>(`${this.baseApiUrl}/Transactions`);
  }

  getReportTransactions() {
    return this.http.get<ReportTransactionModel[]>(`${this.baseApiUrl}/Transactions/ReportTransaction`);
  }

  saveTransaction(transaction: TransactionModel) {
    if (transaction.id) {
      return this.http.put(`${this.baseApiUrl}/Transactions/${transaction.id}`, transaction);
    }

    return this.http.post(`${this.baseApiUrl}/Transactions`, transaction);
  }

  deleteTransaction(id: string) {
    return this.http.delete(`${this.baseApiUrl}/Transactions/${id}`);
  }
}
