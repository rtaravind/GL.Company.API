using GL.Company.DataAccess.Data;
using GL.Company.DataAccess.Interfaces;
using GL.Company.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GL.Company.DataAccess.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TlCompany>> GetCompaniesAsync(CancellationToken cancellationToken) 
            => await _context.Companies.ToListAsync(cancellationToken);

        public async Task<TlCompany?> GetCompanyByIdAsync(int id, CancellationToken cancellationToken) 
            => await _context.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<TlCompany?> GetCompanyByIsinAsync(string isin, CancellationToken cancellationToken) 
            => await _context.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Isin == isin, cancellationToken);

        public async Task<TlCompany> AddCompanyAsync(TlCompany company, CancellationToken cancellationToken)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync(cancellationToken);
            return company;
        }

        public async Task<TlCompany> UpdateCompanyAsync(TlCompany company, CancellationToken cancellationToken)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync(cancellationToken);
            return company;
        }
    }
}
