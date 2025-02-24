using GL.Company.DataAccess.Models;

namespace GL.Company.BLL.Company.CreateCompany
{
    public interface ICreateCompanyUseCase
    {
        Task<TlCompany> ExecuteAsync(CreateCompanyInput input, CancellationToken cancellationToken);
    }
}
