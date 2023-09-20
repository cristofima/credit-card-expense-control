import { Injectable } from '@angular/core';
import { ReportTransactionModel, TransactionModel } from '../models/transaction.model';
import { CreditCardService } from './credit-card.service';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private creditCardService: CreditCardService) { }

  private storageKey = 'transactions';
  private creditCards = this.creditCardService.getCreditCards();

  getTransactions() {
    let transactionsFromLS = localStorage.getItem(this.storageKey);
    return transactionsFromLS ? JSON.parse(transactionsFromLS) as TransactionModel[] : [];
  }

  getReportTransactions() {
    let transactions = this.getTransactions();
    return transactions.map(transaction => {
      const creditCard = this.creditCards.find(x => x.id == transaction.creditCardId);
      const rt: ReportTransactionModel = {
        id: transaction.id,
        creditCard: {
          id: creditCard!.id,
          name: creditCard!.name
        },
        date: transaction.date,
        description: transaction.description,
        amount: transaction.amount,
        quotas: transaction.quotas
      };

      return rt;
    })
  }

  saveTransaction(transaction: TransactionModel) {
    const transactions = this.getTransactions();
    let t = transactions.find(x => x.id == transaction.id);
    if (t) {
      t.creditCardId = transaction.creditCardId;
      t.date = transaction.date;
      t.description = transaction.description;
      t.amount = transaction.amount;
      t.quotas = transaction.quotas;
    } else {
      transactions.push(transaction);
    }

    localStorage.setItem(this.storageKey, JSON.stringify(transactions));
  }

  deleteTransaction(id: string) {
    const transactions = this.getTransactions();
    let index = transactions.findIndex(x => x.id == id);
    if (index >= 0) {
      transactions.splice(index, 1);
      localStorage.setItem(this.storageKey, JSON.stringify(transactions));
    }
  }
}
