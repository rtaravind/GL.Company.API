using GL.Company.BLL.Company.GetCompanyById;
using GL.Company.BLL.Company.GetCompanyByIsin;
using GL.Company.BLL.Company.GetCompanyList;
using GL.Company.DataAccess.Interfaces;
using GL.Company.DataAccess.Models;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace GL.Company.Test.UnitTests.CompanyTests
{
    [Trait("Category", "Unit Test")]
    [Trait("Category", "Read Company")]
    public class CompanyReadTests
    {
        private readonly Mock<ICompanyRepository> _mockRepo;
        private readonly IGetCompanyByIdUseCase _getCompanyByIdUseCase;
        private readonly IGetCompanyByIsinUseCase _getCompanyByIsinUseCase;
        private readonly IGetCompanyListUseCase _getCompanyListUseCase;

        public CompanyReadTests()
        {
            _mockRepo = new Mock<ICompanyRepository>();
            _getCompanyByIdUseCase = new GetCompanyByIdUseCase(_mockRepo.Object);
            _getCompanyByIsinUseCase = new GetCompanyByIsinUseCase(_mockRepo.Object);
            _getCompanyListUseCase = new GetCompanyListUseCase(_mockRepo.Object);
        }

        [Fact]
        public async Task GetCompanyById_ShouldReturnCompanyDetail()
        {
            //Arrange
            var company = new TlCompany
            {
                Name = "Apple",
                Exchange = "NASDAQ",
                Ticker = "AAPL",
                Isin = "US0378331005",
                Website = "http://www.apple.com"
            };

            _mockRepo.Setup(x => x.GetCompanyByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(company);

            //Act 
            var result = await _getCompanyByIdUseCase.ExecuteAsync(1, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Apple", result.Name);
            Assert.Equal("US0378331005", result.Isin);

            _mockRepo.Verify(x => x.GetCompanyByIdAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetCompanyById_ShouldReturnException_WhenCompanyNotFound()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetCompanyByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new ArgumentException("Company not found"));
            //Act & Assert 
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
        _getCompanyByIdUseCase.ExecuteAsync(99, new CancellationToken()));
            Assert.Equal("Company not found", exception.Message);
            _mockRepo.Verify(x => x.GetCompanyByIdAsync(99, It.IsAny<CancellationToken>()), Times.Once);

        }

        [Fact]
        public async Task GetCompanyByIsin_ShouldReturnCompanyDetail()
        {
            //Arrange
            var company = new TlCompany
            {
                Id = 1,
                Name = "Apple",
                Exchange = "NASDAQ",
                Ticker = "AAPL",
                Isin = "US0378331005",
                Website = "http://www.apple.com"
            };

            _mockRepo.Setup(x => x.GetCompanyByIsinAsync("US0378331005", It.IsAny<CancellationToken>())).ReturnsAsync(company);

            //Act 
            var result = await _getCompanyByIsinUseCase.ExecuteAsync("US0378331005", new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Apple", result.Name);
            Assert.Equal("AAPL", result.Ticker);
            _mockRepo.Verify(x => x.GetCompanyByIsinAsync("US0378331005", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetCompanyByIsin_ShouldReturnException_WhenCompanyNotFound()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetCompanyByIsinAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new ArgumentException("Company not found"));
            //Act & Assert 
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
        _getCompanyByIsinUseCase.ExecuteAsync("US03783", new CancellationToken()));
            Assert.Equal("Company not found", exception.Message);
            _mockRepo.Verify(x => x.GetCompanyByIsinAsync("US03783", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetCompanyList_ShouldReturnAllCompanies()
        {
            //Arrange
            var companies = new List<TlCompany> 
            { 
                new TlCompany { Id = 1, Name = "Apple Inc.", Isin = "US0378331005" }, 
                new TlCompany { Id = 2, Name = "Microsoft", Isin = "US5949181045" } 
            };

            _mockRepo.Setup(x => x.GetCompaniesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(companies);

            //Act 
            var result = await _getCompanyListUseCase.ExecuteAsync(new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
