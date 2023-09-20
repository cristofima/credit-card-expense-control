export interface TransactionModel {
  id: string;
  creditCardId: string;
  date: Date;
  description: string;
  amount: number;
  quotas: number;
}

export interface ReportTransactionModel {
  id: string;
  creditCard: {
    id: string;
    name: string;
  }
  date: Date;
  description: string;
  amount: number;
  quotas: number;
}
