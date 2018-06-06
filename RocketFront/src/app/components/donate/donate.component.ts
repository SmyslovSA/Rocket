import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../../services/payment.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-donate',
  templateUrl: './donate.component.html',
  styleUrls: ['./donate.component.css']
})
export class DonateComponent implements OnInit {

    paymentEnabled: boolean;

    constructor(private paymentService: PaymentService) { }

    ngOnInit() {
        this.getPAymentEnabled();
    }

    getPAymentEnabled() {
        this.paymentService.getPAymentEnabled()
            .subscribe(data => {
                this.paymentEnabled = data;
            });
    }

}
