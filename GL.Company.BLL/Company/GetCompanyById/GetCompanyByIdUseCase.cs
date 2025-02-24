using GL.Company.BLL.Company.Mapper;
using GL.Company.DataAccess.Interfaces;

namespace GL.Company.BLL.Company.GetCompanyById
{
    public class GetCompanyByIdUseCase : IGetCompanyByIdUseCase
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyByIdUseCase(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDetailResponse> ExecuteAsync(int companyId, CancellationToken cancellationToken)
        {
            if (companyId <= 0)
            {
                throw new ArgumentException("Company Id is requrired");
            }

            var company =  await _companyRepository.GetCompanyByIdAsync(companyId, cancellationToken);

            if (company == null)
            {
                throw new ArgumentException("Company not found");
            }

            return company.Map();
        }
    }
}
