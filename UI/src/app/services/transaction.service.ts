import { Injectable } from '@angular/core';
import { ReportTransactionModel, TransactionModel } from '../models/transaction.model';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { CreditCardService } from './credit-card.service';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private http: HttpClient, private creditCardService: CreditCardService) { }

  private baseApiUrl = 'http://localhost:5021/api';


  getTransactions() {
    return this.http.get<TransactionModel[]>(`${this.baseApiUrl}/Transactions`);
  }

  async getReportTransactions() {
    let creditCards = await lastValueFrom(this.creditCardService.getCreditCards());
    let transactions = await lastValueFrom(this.getTransactions());
    return transactions.map(transaction => {
      const creditCard = creditCards.find(x => x.id == transaction.creditCardId);
      const rt: ReportTransactionModel = {
        id: transaction.id,
        creditCard: {
          id: creditCard!.id as string,
          name: creditCard!.name,
          brand: creditCard!.brand,
          last4Digits: creditCard!.last4Digits
        },
        date: transaction.date,
        description: transaction.description,
        amount: transaction.amount,
        quotas: transaction.quotas,
        aproxMonthlyQuota: transaction.amount / transaction.quotas
      };

      return rt;
    })
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
