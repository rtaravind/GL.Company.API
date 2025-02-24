using GL.Company.BLL.Company.Mapper;
using GL.Company.DataAccess.Interfaces;

namespace GL.Company.BLL.Company.GetCompanyByIsin
{
    public class GetCompanyByIsinUseCase : IGetCompanyByIsinUseCase
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyByIsinUseCase(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDetailResponse> ExecuteAsync(string isin, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(isin))
            {
                throw new ArgumentException("Isin value is requrired");
            }

            var company = await _companyRepository.GetCompanyByIsinAsync(isin, cancellationToken);

            if (company == null)
            {
                throw new ArgumentException("Company not found");
            }
            return company.Map();
        }
    }
}
