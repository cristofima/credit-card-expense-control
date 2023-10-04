import { Injectable } from '@angular/core';
import { CreditCardModel } from '../models/credit-card.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CreditCardService {

  constructor(private http: HttpClient) { }

  private baseApiUrl = 'http://localhost:27972/api';

  getCreditCards() {
    return this.http.get<CreditCardModel[]>(`${this.baseApiUrl}/CreditCards`);
  }

  saveCreditCard(creditCard: CreditCardModel) {
    if (creditCard.id) {
      return this.http.put(`${this.baseApiUrl}/CreditCards/${creditCard.id}`, creditCard);
    }

    return this.http.post(`${this.baseApiUrl}/CreditCards`, creditCard);
  }

  deleteCreditCard(id: string) {
    return this.http.delete(`${this.baseApiUrl}/CreditCards/${id}`);
  }
}
