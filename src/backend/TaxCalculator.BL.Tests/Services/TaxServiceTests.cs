using AutoMapper;
using Moq;
using TaxCalculator.BL.Interfaces;
using TaxCalculator.BL.Services;
using TaxCalculator.BL.Validator;
using TaxCalculator.Common.Implementation;
using TaxCalculator.DbModels;
using TaxCalculator.InternalModels;

namespace TaxCalculator.BL.Tests.Services
{
    public class TaxServiceTests
    {
        private readonly ITaxService _taxService;
        private readonly Mock<ITaxBandCRUDService> _taxBandCRUDService = new Mock<ITaxBandCRUDService>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        public TaxServiceTests()
        {
            var validator = new TaxBandValidator(new ErrorManager<TaxBandErrorEnum>());
            _taxService = new TaxService(_taxBandCRUDService.Object, validator, _mapper.Object);
        }

        [Fact]
        public async Task CalculateTaxesAsync_ValidatesSalaryAndCalculatesTaxes()
        {
            int salary = 40000;
            var precision = 2;
            var taxBands = new List<TaxBand>
            {
                new TaxBand { LowerBound = 0, UpperBound = 5000, RateInPercents = 0 },
                new TaxBand { LowerBound = 5000, UpperBound = 20000, RateInPercents = 20 },
                new TaxBand { LowerBound = 20000, UpperBound = int.MaxValue, RateInPercents = 40 }
            };

            _taxBandCRUDService.Setup(s => s.GetTaxBandsToCalculateByLowerBoundAsync(salary)).ReturnsAsync(taxBands);

            var result = await _taxService.CalculateTaxesAsync(salary);

            Assert.NotNull(result);
            Assert.Equal(salary, result.GrossAnnualSalary, precision);
            Assert.Equal(3333.33m, result.GrossMonthlySalary, precision);
            Assert.Equal(29000m, result.NetAnnualSalary, precision);
            Assert.Equal(2416.67m, result.NetMonthlySalary, precision);
            Assert.Equal(11000m, result.AnnualTaxPaid, precision);
            Assert.Equal(916.67m, result.MonthlyTaxPaid, precision);
        }

        [Fact]
        public async Task GetTaxBandAsync_MapsAndReturnsTaxBand()
        {
            var id = Guid.NewGuid();
            var taxBand = new TaxBand();
            _taxBandCRUDService.Setup(s => s.ReadItemByIdAsync(id)).ReturnsAsync(taxBand);
            var internalTaxBand = new InternalTaxBand();
            _mapper.Setup(m => m.Map<InternalTaxBand>(It.IsAny<TaxBand>())).Returns(internalTaxBand);

            var result = await _taxService.GetTaxBandAsync(id);

            _mapper.Verify(m => m.Map<InternalTaxBand>(taxBand), Times.Once);
            Assert.Equal(internalTaxBand, result);
        }

        [Fact]
        public async Task GetAllTaxBandsAsync_MapsAndReturnsAllTaxBands()
        {
            var taxBands = new List<TaxBand> { new TaxBand(), new TaxBand() };
            _taxBandCRUDService.Setup(s => s.ReadItemsAsync()).ReturnsAsync(taxBands);
            var internalTaxBands = new List<InternalTaxBand> { new InternalTaxBand(), new InternalTaxBand() };
            _mapper.Setup(m => m.Map<IEnumerable<InternalTaxBand>>(It.IsAny<IEnumerable<TaxBand>>())).Returns(internalTaxBands);

            var result = await _taxService.GetAllTaxBandsAsync();

            _mapper.Verify(m => m.Map<IEnumerable<InternalTaxBand>>(taxBands), Times.Once);
            Assert.Equal(internalTaxBands, result);
        }

        [Fact]
        public async Task CreateTaxBandAsync_CreatesAndReturnsTaxBand()
        {
            var internalTaxBand = new InternalTaxBand();
            var taxBand = new TaxBand();
            _mapper.Setup(m => m.Map<TaxBand>(internalTaxBand)).Returns(taxBand);

            var result = await _taxService.CreateTaxBandAsync(internalTaxBand);

            _mapper.Verify(m => m.Map<TaxBand>(internalTaxBand), Times.Once);
            _taxBandCRUDService.Verify(s => s.CreateAsync(taxBand), Times.Once);
            Assert.Equal(internalTaxBand, result);
        }

        [Fact]
        public async Task UpdateTaxBandAsync_UpdatesAndReturnsTaxBand()
        {
            var id = Guid.NewGuid();
            var internalTaxBand = new InternalTaxBand();
            var taxBand = new TaxBand();
            _mapper.Setup(m => m.Map<TaxBand>(internalTaxBand)).Returns(taxBand);

            var result = await _taxService.UpdateTaxBandAsync(id, internalTaxBand);

            _mapper.Verify(m => m.Map<TaxBand>(internalTaxBand), Times.Once);
            _taxBandCRUDService.Verify(s => s.UpdateAsync(It.IsAny<TaxBand>()), Times.Once);
            Assert.Equal(internalTaxBand, result);
        }

        [Fact]
        public async Task RemoveTaxBandAsync_RemovesTaxBand()
        {
            var internalTaxBand = new InternalTaxBand();
            var taxBand = new TaxBand();
            _mapper.Setup(m => m.Map<TaxBand>(internalTaxBand)).Returns(taxBand);

            var result = await _taxService.RemoveTaxBandAsync(internalTaxBand);

            _mapper.Verify(m => m.Map<TaxBand>(internalTaxBand), Times.Once);
            _taxBandCRUDService.Verify(s => s.DeleteAsync(taxBand), Times.Once);
            Assert.Equal(internalTaxBand, result);
        }

        [Fact]
        public async Task RemoveTaxBandAsync_ByIdReadsAndRemovesTaxBand()
        {
            var id = Guid.NewGuid();
            var taxBand = new TaxBand { Id = id };
            _taxBandCRUDService.Setup(s => s.DeleteByIdAsync(id));

            await _taxService.RemoveTaxBandAsync(id);

            _taxBandCRUDService.Verify(s => s.DeleteByIdAsync(id), Times.Once);
        }
    }
}
