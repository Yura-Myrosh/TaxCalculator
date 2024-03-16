import { Component, Input } from '@angular/core';
import { TaxDetails } from '../tax-details';
import { MatListModule } from '@angular/material/list';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tax-details',
  standalone: true,
  imports: [CommonModule, MatListModule],
  templateUrl: './tax-details.component.html',
  styleUrl: './tax-details.component.css'
})
export class TaxDetailsComponent {
@Input() taxDetails? : TaxDetails;
}
