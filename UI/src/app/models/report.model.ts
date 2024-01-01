export interface MinimalReportModel {
  january: number;
  february: number;
  march: number;
  april: number;
  may: number;
  june: number;
  july: number;
  august: number;
  september: number;
  october: number;
  november: number;
  december: number;
  totalAnnual?: number;
}

export interface ReportModel extends MinimalReportModel {
  creditCard: {
    id: string;
    name: string;
    brand: string;
    last4Digits: string;
  }
}
