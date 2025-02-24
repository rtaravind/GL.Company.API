using GL.Company.DataAccess.Interfaces;
using GL.Company.DataAccess.Models;

namespace GL.Company.BLL.Company.CreateCompany
{
    public class CreateCompanyUseCase : ICreateCompanyUseCase
    {
        private readonly ICompanyRepository _companyRepository;
        public CreateCompanyUseCase(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<TlCompany> ExecuteAsync(CreateCompanyInput input, CancellationToken cancellationToken)
        {
            ValidateInput(input);
            
            var company = await _companyRepository.GetCompanyByIsinAsync(input.Isin,cancellationToken);
            if ( company!= null)
            {
                throw new ArgumentException("A company with this ISIN already exists.");
            }

            return await _companyRepository.AddCompanyAsync(new TlCompany
            { 
                Name = input.CompanyName,
                Exchange = input.Exchange,
                Isin = input.Isin,
                Ticker = input.Ticker,
                Website = input.Website
            }, cancellationToken);

        }

        private void ValidateInput(CreateCompanyInput input)
        {
            if (string.IsNullOrEmpty(input.CompanyName))
            {
                throw new ArgumentException("Name is required.");
            }

            if (string.IsNullOrEmpty(input.Exchange))
            {
                throw new ArgumentException("Exchange is required.");
            }

            if (string.IsNullOrEmpty(input.Ticker))
            {
                throw new ArgumentException("Ticker is required.");
            }

            if (string.IsNullOrEmpty(input.Isin))
            {
                throw new ArgumentException("ISIN is required.");
            }

            if (!char.IsLetter(input.Isin[0]) || !char.IsLetter(input.Isin[1]))
            {
                throw new ArgumentException("The first two characters of an ISIN must be letters.");
            }
        }
    }
}
