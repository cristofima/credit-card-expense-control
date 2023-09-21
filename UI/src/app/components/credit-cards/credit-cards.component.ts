import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'js-guid';
import { MessageService, SelectItem } from 'primeng/api';
import { CreditCardModel } from 'src/app/models/credit-card.model';
import { CreditCardService } from 'src/app/services/credit-card.service';

@Component({
  selector: 'app-credit-cards',
  templateUrl: './credit-cards.component.html',
  styleUrls: ['./credit-cards.component.scss']
})
export class CreditCardsComponent implements OnInit {

  constructor(private creditCardService: CreditCardService, private messageService: MessageService, private formBuilder: FormBuilder) { }

  creditCards: CreditCardModel[] = [];
  visible = false;
  isEdit = false;
  private selectedCreditCard?: CreditCardModel;
  nonNegativeNumbers: RegExp = /\d+/;

  formGroup!: FormGroup;
  creditCardBrandOptions: SelectItem[] = [];

  ngOnInit(): void {
    this.creditCards = this.creditCardService.getCreditCards();
    this.buildCreditCardBrandOptions();

    this.formGroup = this.formBuilder.group({
      name: new FormControl('', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(25)])),
      brand: new FormControl('', Validators.compose([Validators.required])),
      expirationMonth: new FormControl('', Validators.compose([Validators.required])),
      expirationYear: new FormControl('', Validators.compose([Validators.required])),
      last4Digits: new FormControl('', Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(4)])),
      cutOffDate: new FormControl('', Validators.compose([Validators.required]))
    });
  }

  private buildCreditCardBrandOptions() {
    const brands = ["Visa", "Mastercard", "Diners Club", "Discover"];
    brands.forEach(b => {
      this.creditCardBrandOptions.push({
        value: b,
        label: b
      });
    });
  }

  showDialog(creditCard?: CreditCardModel) {
    this.visible = true;
    this.isEdit = !!creditCard;
    this.selectedCreditCard = creditCard;

    if (!creditCard) {
      this.formGroup.reset();
      return;
    }

    this.formGroup.setValue({
      name: creditCard.name,
      brand: creditCard.brand,
      cutOffDate: creditCard.cutOffDate,
      expirationMonth: creditCard.expirationMonth,
      expirationYear: creditCard.expirationYear,
      last4Digits: creditCard.last4Digits
    });
  }

  saveCreditCard() {
    this.visible = false;
    let creditCard: CreditCardModel = {
      id: this.isEdit ? this.selectedCreditCard!.id : Guid.newGuid().toString(),
      name: this.formGroup.controls['name'].value,
      brand: this.formGroup.controls['brand'].value,
      cutOffDate: this.formGroup.controls['cutOffDate'].value,
      expirationMonth: this.formGroup.controls['expirationMonth'].value,
      expirationYear: this.formGroup.controls['expirationYear'].value,
      last4Digits: this.formGroup.controls['last4Digits'].value
    };

    this.creditCardService.saveCreditCard(creditCard);
    this.messageService.add({ severity: 'success', summary: 'Confirmación', detail: `Tarjeta ${creditCard.name} ${this.isEdit ? 'actualizada' : 'agregada'}` });
    this.creditCards = this.creditCardService.getCreditCards();
  }
}
