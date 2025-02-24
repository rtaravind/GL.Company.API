using GL.Company.DataAccess.Models;

namespace GL.Company.DataAccess.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<TlCompany>> GetCompaniesAsync(CancellationToken cancellationToken);
        Task<TlCompany?> GetCompanyByIdAsync(int id, CancellationToken cancellationToken);
        Task<TlCompany?> GetCompanyByIsinAsync(string isin, CancellationToken cancellationToken);
        Task<TlCompany> AddCompanyAsync(TlCompany company, CancellationToken cancellationToken);
        Task<TlCompany> UpdateCompanyAsync(TlCompany company, CancellationToken cancellationToken);
    }
}
