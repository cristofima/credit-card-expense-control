export interface TransactionModel {
  id?: string;
  creditCardId: string;
  date: Date;
  description: string;
  amount: number;
  quotas: number;
  graceMonths: number;
  isRecurringPayment: boolean;
  recurringPaymentEndDate?: Date;
}

export interface ReportTransactionModel {
  id?: string;
  creditCard: {
    id: string;
    name: string;
    last4Digits: string;
    brand: string;
  }
  date: Date;
  description: string;
  amount: number;
  quotas: number;
  graceMonths: number;
  aproxMonthlyQuota: number;
  isRecurringPayment: boolean;
  recurringPaymentEndDate?: Date;
}
