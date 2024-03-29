export interface CreditCardModel {
  id?: string;
  name: string;
  brand: string;
  expirationMonth: number;
  expirationYear: number;
  last4Digits: string;
  cutOffDay: number;
  deferredContributionPercentage: number;
}
