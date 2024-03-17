using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.BL.Validator
{
    public interface ITaxBandValidator
    {
        void ValidateBounds(int lowerBound, int upperBound);
        public void ValidateSalary(int salary);
    }
}
