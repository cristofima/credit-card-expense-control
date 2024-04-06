import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { lastValueFrom } from 'rxjs';
import { CreditCardModel } from 'src/app/models/credit-card.model';
import { ReportTransactionModel, TransactionModel } from 'src/app/models/transaction.model';
import { CreditCardService } from 'src/app/services/credit-card.service';
import { TransactionService } from 'src/app/services/transaction.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.scss']
})
export class TransactionsComponent implements OnInit {

  constructor(private creditCardService: CreditCardService, private transactionService: TransactionService, private confirmationService: ConfirmationService,
    private messageService: MessageService, private formBuilder: FormBuilder) { }

  creditCards: CreditCardModel[] = [];
  transactions: ReportTransactionModel[] = [];
  visible = false;
  isEdit = false;
  loading = false;
  maxDate: Date = new Date();

  transactionTypes: any[] = [
    { name: 'Corriente', key: 'C' },
    { name: 'Diferido', key: 'D' }
  ];

  private selectedTransaction?: ReportTransactionModel;

  formGroup!: FormGroup;

  ngOnInit(): void {
    this.creditCardService.getCreditCards().subscribe(creditCards => {
      this.creditCards = creditCards;
    });

    this.formGroup = this.formBuilder.group({
      description: new FormControl('', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(25)])),
      creditCardId: new FormControl('', Validators.compose([Validators.required])),
      date: new FormControl('', Validators.compose([Validators.required])),
      amount: new FormControl('', Validators.compose([Validators.required])),
      transactionType: new FormControl('', Validators.compose([Validators.required])),
      quotas: new FormControl(1, {
        initialValueIsDefault: true, validators: Validators.compose([Validators.required, Validators.min(1)])
      }),
      graceMonths: new FormControl(0, {
        initialValueIsDefault: true, validators: Validators.compose([Validators.required, Validators.min(0)])
      }),
      isRecurringPayment: new FormControl(false, {
        initialValueIsDefault: true
      }),
      isCashAdvance: new FormControl(false, {
        initialValueIsDefault: true
      }),
      cashAdvanceFee: new FormControl(0, {
        initialValueIsDefault: true, validators: Validators.compose([Validators.min(0)])
      }),
      totalAmountTransaction: new FormControl(null),
      recurringPaymentEndDate: new FormControl(''),
    });

    this.initTransactions();
  }

  private async initTransactions() {
    this.loading = true;
    this.transactions = await lastValueFrom(this.transactionService.getReportTransactions());
    this.loading = false;
  }

  showDialog(transaction?: ReportTransactionModel) {
    this.visible = true;
    this.isEdit = !!transaction;
    this.selectedTransaction = transaction;

    if (!transaction) {
      this.formGroup.reset();
      this.formGroup.controls['transactionType'].setValue(this.transactionTypes[0]);
      return;
    }

    this.formGroup.setValue({
      description: transaction.description,
      creditCardId: transaction.creditCard.id,
      date: new Date(transaction.date),
      amount: transaction.amount,
      quotas: transaction.quotas,
      graceMonths: transaction.graceMonths,
      transactionType: transaction.quotas > 1 ? this.transactionTypes[1] : this.transactionTypes[0],
      isRecurringPayment: transaction.isRecurringPayment,
      isCashAdvance: transaction.isCashAdvance,
      cashAdvanceFee: transaction.isCashAdvance ? transaction.cashAdvanceFee : null,
      totalAmountTransaction: transaction.isCashAdvance ? transaction.totalAmountTransaction : null,
      recurringPaymentEndDate: transaction.recurringPaymentEndDate ? new Date(transaction.recurringPaymentEndDate) : null
    });
  }

  saveTransaction() {
    this.visible = false;

    let isCurrent = this.formGroup.controls['transactionType'].value.key == 'C';

    let transaction: TransactionModel = {
      creditCardId: this.formGroup.controls['creditCardId'].value,
      date: this.formGroup.controls['date'].value,
      description: this.formGroup.controls['description'].value,
      amount: this.formGroup.controls['amount'].value,
      quotas: isCurrent ? 1 : this.formGroup.controls['quotas'].value,
      graceMonths: isCurrent ? 0 : this.formGroup.controls['graceMonths'].value,
      isRecurringPayment: this.formGroup.controls['isRecurringPayment'].value,
      isCashAdvance: this.formGroup.controls['isCashAdvance'].value
    };

    if (transaction.isRecurringPayment) {
      transaction.recurringPaymentEndDate = this.formGroup.controls['recurringPaymentEndDate'].value;
    }

    if (transaction.isCashAdvance) {
      transaction.cashAdvanceFee = this.formGroup.controls['cashAdvanceFee'].value;
      transaction.totalAmountTransaction = this.formGroup.controls['totalAmountTransaction'].value;
    }

    if (this.isEdit) {
      transaction.id = this.selectedTransaction!.id;
    }

    this.transactionService.saveTransaction(transaction).subscribe(() => {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: `Transacción ${this.isEdit ? 'actualizada' : 'agregada'}` });
      this.initTransactions();
    });
  }

  deleteTransaction(transaction: ReportTransactionModel) {
    this.confirmationService.confirm({
      message: `¿Está seguro de eliminar la transacción?`,
      header: 'Confirmación',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.transactionService.deleteTransaction(transaction.id as string).subscribe(() => {
          this.transactions = this.transactions.filter(x => x.id != transaction.id);
          this.messageService.add({ severity: 'success', summary: 'Confirmación', detail: `Transacción eliminada` });
        });
      }
    });
  }
}
