import { Injectable } from '@angular/core';
import { CreditCardModel } from '../models/credit-card.model';

@Injectable({
  providedIn: 'root'
})
export class CreditCardService {

  private storageKey = 'credit-cards';

  getCreditCards() {
    const creditCards = localStorage.getItem(this.storageKey);
    return creditCards ? JSON.parse(creditCards) as CreditCardModel[] : [];
  }

  saveCreditCard(creditCard: CreditCardModel) {
    const creditCards = this.getCreditCards();
    let cd = creditCards.find(x => x.id == creditCard.id);
    if (cd) {
      cd.name = creditCard.name;
      cd.brand = creditCard.brand;
      cd.expirationMonth = creditCard.expirationMonth;
      cd.expirationYear = creditCard.expirationYear;
      cd.last4Digits = creditCard.last4Digits;
      cd.cutOffDate = creditCard.cutOffDate;
    } else {
      creditCards.push(creditCard);
    }

    localStorage.setItem(this.storageKey, JSON.stringify(creditCards));
  }
}
