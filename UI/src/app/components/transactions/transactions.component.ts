import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'js-guid';
import { ConfirmationService, MessageService } from 'primeng/api';
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
  private selectedTransaction?: ReportTransactionModel;

  formGroup!: FormGroup;

  ngOnInit(): void {
    this.creditCards = this.creditCardService.getCreditCards();
    this.transactions = this.transactionService.getReportTransactions();

    this.formGroup = this.formBuilder.group({
      description: new FormControl('', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(25)])),
      creditCardId: new FormControl('', Validators.compose([Validators.required])),
      date: new FormControl('', Validators.compose([Validators.required])),
      amount: new FormControl('', Validators.compose([Validators.required])),
      quotas: new FormControl('', Validators.compose([Validators.required]))
    });
  }

  showDialog(transaction?: ReportTransactionModel) {
    this.visible = true;
    this.isEdit = !!transaction;
    this.selectedTransaction = transaction;

    if (!transaction) {
      this.formGroup.reset();
      return;
    }

    this.formGroup.setValue({
      description: transaction.description,
      creditCardId: transaction.creditCard.id,
      date: new Date(transaction.date),
      amount: transaction.amount,
      quotas: transaction.quotas
    });
  }

  saveTransaction() {
    this.visible = false;
    let transaction: TransactionModel = {
      id: this.isEdit ? this.selectedTransaction!.id : Guid.newGuid().toString(),
      creditCardId: this.formGroup.controls['creditCardId'].value,
      date: this.formGroup.controls['date'].value,
      description: this.formGroup.controls['description'].value,
      amount: this.formGroup.controls['amount'].value,
      quotas: this.formGroup.controls['quotas'].value
    };

    this.transactionService.saveTransaction(transaction);
    this.transactions = this.transactionService.getReportTransactions();

    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Transaction saved successfully' });
  }

  deleteTransaction(transaction: ReportTransactionModel) {
    this.confirmationService.confirm({
      message: `¿Está seguro de eliminar la transacción?`,
      header: 'Confirmación',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.transactionService.deleteTransaction(transaction.id);
        this.transactions = this.transactions.filter(x => x.id != transaction.id);
        this.messageService.add({ severity: 'success', summary: 'Confirmación', detail: `Transacción eliminada` });
      }
    });
  }
}
