using GL.Company.BLL.Company.CreateCompany;
using GL.Company.BLL.Company.UpdateCompany;
using GL.Company.DataAccess.Interfaces;
using GL.Company.DataAccess.Models;
using Moq;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Assert = Xunit.Assert;

namespace GL.Company.Test.UnitTests.CompanyTests
{
    [Trait("Category", "Unit Test")]
    [Trait("Category", "Write Company")]
    public class CompanyWriteTests
    {
        private readonly Mock<ICompanyRepository> _mockRepo;
        private readonly ICreateCompanyUseCase _createCompanyUseCase;
        private readonly IUpdateCompanyUseCase _updateCompanyUseCase;

        public CompanyWriteTests()
        {
            _mockRepo = new Mock<ICompanyRepository>();
            _createCompanyUseCase = new CreateCompanyUseCase(_mockRepo.Object);
            _updateCompanyUseCase = new UpdateCompanyUseCase(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateCompanyAsync_ShouldCreateCompany_WhenValid()
        {
            // Arrange
            var input = new CreateCompanyInput { CompanyName = "Apple Inc.", Exchange = "NYSE", Isin = "US0378331005", Ticker = "AAPL" };
            var newCompany = new TlCompany { Id = 1, Name = "Apple Inc.", Exchange = "NYSE", Isin = "US0378331005", Ticker = "AAPL" };
            _mockRepo.Setup(repo => repo.AddCompanyAsync(It.IsAny<TlCompany>(), It.IsAny<CancellationToken>())).ReturnsAsync(newCompany);

            // Act
            var result = await _createCompanyUseCase.ExecuteAsync(input, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Apple Inc.", result.Name);
            _mockRepo.Verify(repo => repo.AddCompanyAsync(It.IsAny<TlCompany>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateCompanyAsync_NameNull_ShouldReturnError()
        {
            // Arrange
            var input = new CreateCompanyInput { CompanyName = "", Exchange = "NYSE", Isin = "US0378331005", Ticker = "AAPL" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
        _createCompanyUseCase.ExecuteAsync(input, new CancellationToken()));

            Assert.Equal("Name is required.", exception.Message);
            _mockRepo.Verify(repo => repo.AddCompanyAsync(It.IsAny<TlCompany>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task CreateCompanyAsync_ExchangeNull_ShouldReturnError()
        {
            // Arrange
            var input = new CreateCompanyInput { CompanyName = "Apple Inc.", Exchange = "", Isin = "US0378331005", Ticker = "AAPL" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
        _createCompanyUseCase.ExecuteAsync(input, new CancellationToken()));

            Assert.Equal("Exchange is required.", exception.Message);
            _mockRepo.Verify(repo => repo.AddCompanyAsync(It.IsAny<TlCompany>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task CreateCompanyAsync_TickerNull_ShouldReturnError()
        {
            // Arrange
            var input = new CreateCompanyInput { CompanyName = "Apple Inc.", Exchange = "NYSE", Isin = "US0378331005", Ticker = "" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
        _createCompanyUseCase.ExecuteAsync(input, new CancellationToken()));

            Assert.Equal("Ticker is required.", exception.Message);
            _mockRepo.Verify(repo => repo.AddCompanyAsync(It.IsAny<TlCompany>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task CreateCompanyAsync_IsinNull_ShouldReturnError()
        {
            // Arrange
            var input = new CreateCompanyInput { CompanyName = "Apple Inc.", Exchange = "NYSE", Isin = "", Ticker = "AAPL" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
        _createCompanyUseCase.ExecuteAsync(input, new CancellationToken()));

            Assert.Equal("ISIN is required.", exception.Message);
            _mockRepo.Verify(repo => repo.AddCompanyAsync(It.IsAny<TlCompany>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task CreateCompanyAsync_IsinInvalid_ShouldReturnError()
        {
            // Arrange
            var input = new CreateCompanyInput { CompanyName = "Apple Inc.", Exchange = "NYSE", Isin = "1234", Ticker = "AAPL" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
        _createCompanyUseCase.ExecuteAsync(input, new CancellationToken()));

            Assert.Equal("The first two characters of an ISIN must be letters.", exception.Message);
            _mockRepo.Verify(repo => repo.AddCompanyAsync(It.IsAny<TlCompany>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCompanyAsync_ShouldReturnUpdatedCompany_WhenValid()
        {
            // Arrange
            var input = new UpdateCompanyDetail { Id = 1, CompanyName = "Apple Inc.", Exchange = "NYSE", Isin = "US0378331005", Ticker = "AAPL" };
            var existingCompany = new TlCompany { Id = 1, Name = "Apple Inc.", Isin = "US0378331005" };
            var updatedCompany = new TlCompany { Id = 1, Name = "Apple", Isin = "US0378331005" };
            _mockRepo.Setup(x => x.GetCompanyByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(existingCompany);
            _mockRepo.Setup(repo => repo.UpdateCompanyAsync(It.IsAny<TlCompany>(), It.IsAny<CancellationToken>())).ReturnsAsync(updatedCompany);

            // Act
            var result = await _updateCompanyUseCase.ExecuteAsync(input, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Apple", result.Name);
            _mockRepo.Verify(repo => repo.UpdateCompanyAsync(It.IsAny<TlCompany>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCompanyAsync_CompanyNotFound_ShouldReturnError()
        {
            // Arrange
            var input = new UpdateCompanyDetail { Id = 99, CompanyName = "Apple Inc.", Exchange = "NYSE", Isin = "US0378331005", Ticker = "AAPL" };
            _mockRepo.Setup(x => x.GetCompanyByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new ArgumentException("Company not found."));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
        _updateCompanyUseCase.ExecuteAsync(input, new CancellationToken()));

            Assert.Equal("Company not found.", exception.Message);
            _mockRepo.Verify(x => x.GetCompanyByIdAsync(99, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(repo => repo.UpdateCompanyAsync(It.IsAny<TlCompany>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCompanyAsync_CompanyWithIsinExist_ShouldReturnError()
        {
            // Arrange
            var existingCompany = new TlCompany { Id = 1, Name = "Apple Inc.", Isin = "US0378331005" };
            var input = new UpdateCompanyDetail { Id = 1, CompanyName = "Apple Inc.", Exchange = "NYSE", Isin = "US0477829223", Ticker = "AAPL" };
            var existingCompanyWithIsin = new TlCompany { Id = 2, Name = "Microsoft", Exchange = "NYSE", Isin = "US0477829223", Ticker = "MSFT" };
            _mockRepo.Setup(x => x.GetCompanyByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(existingCompany);
            _mockRepo.Setup(x => x.GetCompanyByIsinAsync("US0477829223", It.IsAny<CancellationToken>())).ReturnsAsync(existingCompanyWithIsin);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
        _updateCompanyUseCase.ExecuteAsync(input, new CancellationToken()));

            Assert.Equal("A company with this ISIN already exists.", exception.Message);
            _mockRepo.Verify(x => x.GetCompanyByIdAsync(1, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(x => x.GetCompanyByIsinAsync("US0477829223", It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(repo => repo.UpdateCompanyAsync(It.IsAny<TlCompany>(), It.IsAny<CancellationToken>()), Times.Never);
        }

    }
}
