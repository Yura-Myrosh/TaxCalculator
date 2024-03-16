import { Component, Input, OnInit } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { TaxCalculatorService } from '../services/tax-calculator.service';
import { TaxDetails } from '../tax-details';
import { TaxDetailsComponent } from '../tax-details/tax-details.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [MatButtonModule, MatInputModule, FormsModule, TaxDetailsComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit {

  constructor(private taxService: TaxCalculatorService){
  }

  ngOnInit(): void {
    this.salary = undefined;
  }

  @Input() salary?: Number;
  taxDetails!: TaxDetails;

  calculateTax() : void{
    if(this.salary !== undefined){
      this.taxService.getTaxDetails(this.salary).subscribe(taxDetails =>{
        this.taxDetails = taxDetails;
      })      
    }
  }
}
