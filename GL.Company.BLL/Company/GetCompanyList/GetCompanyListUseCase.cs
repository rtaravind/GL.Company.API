using GL.Company.BLL.Company.Mapper;
using GL.Company.DataAccess.Interfaces;

namespace GL.Company.BLL.Company.GetCompanyList
{
    public class GetCompanyListUseCase : IGetCompanyListUseCase
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyListUseCase(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<IEnumerable<CompanyDetailResponse>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetCompaniesAsync(cancellationToken);

            return companies.Map();
        }
    }
}
