using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.BL.Models
{
    public class TaxDetails
    {
        public TaxDetails(decimal grossAnnualSalary)
        {
            GrossAnnualSalary = grossAnnualSalary;
        }
        public decimal GrossAnnualSalary { get; set; }
        public decimal GrossMonthlySalary => GrossAnnualSalary / 12;
        public decimal NetAnnualSalary => GrossAnnualSalary - AnnualTaxPaid;
        public decimal NetMonthlySalary => NetAnnualSalary / 12;
        public decimal AnnualTaxPaid { get; set; }
        public decimal MonthlyTaxPaid => AnnualTaxPaid / 12;
    }
}
