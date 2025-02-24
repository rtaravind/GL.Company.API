using GL.Company.DataAccess.Interfaces;
using GL.Company.DataAccess.Models;

namespace GL.Company.BLL.Company.UpdateCompany
{
    public class UpdateCompanyUseCase : IUpdateCompanyUseCase
    {
        private readonly ICompanyRepository _companyRepository;
        public UpdateCompanyUseCase(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<TlCompany> ExecuteAsync(UpdateCompanyDetail input, CancellationToken cancellationToken)
        {
            ValidateInput(input);

            var companyExist = await _companyRepository.GetCompanyByIdAsync(input.Id, cancellationToken);
            if (companyExist == null)
            {
                throw new ArgumentException("Company not found.");
            }

            var companyWithSameIsin = await _companyRepository.GetCompanyByIsinAsync(input.Isin, cancellationToken);
            if (companyWithSameIsin != null && companyWithSameIsin.Id != input.Id)
            {
                throw new ArgumentException("A company with this ISIN already exists.");
            }

            return await _companyRepository.UpdateCompanyAsync(new TlCompany
            { 
                Id = input.Id,
                Name = input.CompanyName,
                Exchange = input.Exchange,
                Isin = input.Isin,
                Ticker = input.Ticker,
                Website = input.Website
            }, cancellationToken);

        }

        private void ValidateInput(UpdateCompanyDetail input)
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
