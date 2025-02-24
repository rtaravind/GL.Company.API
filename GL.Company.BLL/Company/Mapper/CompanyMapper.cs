using GL.Company.DataAccess.Models;

namespace GL.Company.BLL.Company.Mapper
{
    public static class CompanyMapper
    {
        public static CompanyDetailResponse Map(this TlCompany source)
        {
            return new CompanyDetailResponse
            {
                Id = source.Id,
                Name = source.Name,
                Exchange = source.Exchange,
                Isin = source.Isin,
                Ticker = source.Ticker,
                Website = source.Website
            };
        }

        public static IEnumerable<CompanyDetailResponse> Map(this IEnumerable<TlCompany> model)
        => model.Select(Map);
    }
}
