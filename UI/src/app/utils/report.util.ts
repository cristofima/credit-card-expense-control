import { ReportTransactionModel } from "../models/transaction.model";
import * as moment from "moment";

export class ReportUtil {

  static getTransacctionsToBePaid(transactions: ReportTransactionModel[], cutOffDay: number, month: number, year: number) {
    let newTransactions: ReportTransactionModel[] = [];

    transactions.forEach(transaction => {
      let purchaseDate = new Date(transaction.date);
      let purchaseDay = purchaseDate.getDate();
      let purchaseMonth = purchaseDate.getMonth() + 1;
      let purchaseYear = purchaseDate.getFullYear();
      const { firstPaymentMonth, firstPaymentYear } = this.getFirstPaymentMonthYear(month, year, cutOffDay, purchaseYear, purchaseMonth, purchaseDay);

      if (transaction.quotas == 1) {
        if (firstPaymentMonth == month && firstPaymentYear == year) {
          newTransactions.push(transaction);
        }
      } else {
        let firstPaymentDate = new Date(firstPaymentYear, firstPaymentMonth - 1, 1);
        let lastPaymentDate = moment(firstPaymentDate).add(transaction.quotas, 'month').toDate();
        let currentDate = new Date(year, month - 1, 1);

        if (currentDate >= firstPaymentDate && currentDate <= lastPaymentDate) {
          newTransactions.push(transaction);
        }
      }
    });

    return newTransactions;
  }

  private static getFirstPaymentMonthYear(month: number, year: number, cutOffDay: number, purchaseYear: number, purchaseMonth: number, purchaseDay: number) {
    let lastDayOfTheMonth = new Date(year, month, 0).getDate();
    let firstPaymentMonth = 0;
    let firstPaymentYear = year;

    if (purchaseDay <= cutOffDay) {
      if (cutOffDay + 15 <= lastDayOfTheMonth) {
        firstPaymentMonth = purchaseMonth;
        firstPaymentYear = purchaseYear;
      } else {
        if (purchaseMonth == 12) {
          firstPaymentMonth = 1;
          firstPaymentYear = purchaseYear + 1;
        } else {
          firstPaymentMonth = purchaseMonth + 1;
          firstPaymentYear = purchaseYear;
        }
      }
    } else {
      if (cutOffDay + 15 <= lastDayOfTheMonth) {
        firstPaymentMonth = purchaseMonth + 1;
        firstPaymentYear = purchaseYear;
      } else {
        if (purchaseMonth == 12) {
          firstPaymentMonth = 1;
          firstPaymentYear = purchaseYear + 1;
        } else {
          firstPaymentMonth = purchaseMonth + 2;
          firstPaymentYear = purchaseYear;
        }
      }
    }

    return { firstPaymentMonth, firstPaymentYear };
  }
}
