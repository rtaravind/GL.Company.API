namespace GL.Company.BLL.Company.GetCompanyList
{
    public interface IGetCompanyListUseCase
    {
        Task<IEnumerable<CompanyDetailResponse>> ExecuteAsync(CancellationToken cancellationToken);
    }
}
