import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  items: MenuItem[] = [];

  ngOnInit(): void {
    this.items = [
      { label: 'Tarjetas de Crédito', icon: 'pi pi-fw pi-credit-card', routerLink: ['/credit-cards'] },
      // { label: 'Tiendas', icon: 'pi pi-fw pi-shopping-bag', routerLink: ['/stores'] },
      { label: 'Transacciones', icon: 'pi pi-fw pi-list', routerLink: ['/transactions'] },
      { label: 'Reportes', icon: 'pi pi-fw pi-cog', routerLink: ['/reports'] }
    ];
  }
}
