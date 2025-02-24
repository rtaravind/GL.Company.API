namespace GL.Company.BLL.Company.GetCompanyByIsin
{
    public interface IGetCompanyByIsinUseCase
    {
        Task<CompanyDetailResponse> ExecuteAsync(string isin, CancellationToken cancellationToken);
    }
}
