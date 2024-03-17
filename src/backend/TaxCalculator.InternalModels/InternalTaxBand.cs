namespace TaxCalculator.InternalModels
{
    public class InternalTaxBand
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public int LowerBound { get; set; }

        public int UpperBound { get; set; }

        public int RateInPercents { get; set; }
    }
}
