import { Injectable } from '@angular/core';
import { ReportModel } from '../models/report.model';
import { HttpClient } from '@angular/common/http';
import { ReportTransactionModel } from '../models/transaction.model';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient) { }

  private baseApiUrl = 'http://localhost:27972/api';

  getReportByYear(year: number) {
    return this.http.get<ReportModel[]>(`${this.baseApiUrl}/Reports/${year}`);
  }

  getReportTransactionsByYearAndMonth(year: number, month: number) {
    return this.http.get<ReportTransactionModel[]>(`${this.baseApiUrl}/Reports/${year}/${month}`);
  }

}
