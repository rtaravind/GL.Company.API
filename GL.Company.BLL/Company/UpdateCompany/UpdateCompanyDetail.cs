namespace GL.Company.BLL.Company.UpdateCompany
{
    public class UpdateCompanyDetail
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Exchange { get; set; } = string.Empty;
        public string Ticker { get; set; } = string.Empty;
        public string Isin { get; set; } = string.Empty;
        public string? Website { get; set; }
    }
}
