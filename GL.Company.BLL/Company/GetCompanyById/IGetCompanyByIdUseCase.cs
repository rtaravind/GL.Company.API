namespace GL.Company.BLL.Company.GetCompanyById
{
    public interface IGetCompanyByIdUseCase
    {
        Task<CompanyDetailResponse> ExecuteAsync(int companyId, CancellationToken cancellationToken);
    }
}
