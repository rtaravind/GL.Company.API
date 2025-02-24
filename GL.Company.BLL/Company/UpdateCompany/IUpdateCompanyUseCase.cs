using GL.Company.DataAccess.Models;

namespace GL.Company.BLL.Company.UpdateCompany
{
    public interface IUpdateCompanyUseCase
    {
        Task<TlCompany> ExecuteAsync(UpdateCompanyDetail input, CancellationToken cancellationToken);
    }
}
