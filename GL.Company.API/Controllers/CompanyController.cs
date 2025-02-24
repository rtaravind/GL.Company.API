using GL.Company.API.Request;
using GL.Company.BLL.Company.CreateCompany;
using GL.Company.BLL.Company.GetCompanyById;
using GL.Company.BLL.Company.GetCompanyByIsin;
using GL.Company.BLL.Company.GetCompanyList;
using GL.Company.BLL.Company.UpdateCompany;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GL.Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly IGetCompanyByIdUseCase _getCompanyByIdUseCase;
        private readonly IGetCompanyByIsinUseCase _getCompanyByIsinUseCase;
        private readonly IGetCompanyListUseCase _getCompanyListUseCase;
        private readonly ICreateCompanyUseCase _createCompanyUseCase;
        private readonly IUpdateCompanyUseCase _updateCompanyUseCase;

        public CompanyController(IGetCompanyByIdUseCase getCompanyByIdUseCase,
            IGetCompanyByIsinUseCase getCompanyByIsinUseCase,
            IGetCompanyListUseCase getCompanyListUseCase,
            ICreateCompanyUseCase createCompanyUseCase,
            IUpdateCompanyUseCase updateCompanyUseCase)
        {
            _getCompanyByIdUseCase = getCompanyByIdUseCase;
            _getCompanyByIsinUseCase = getCompanyByIsinUseCase;
            _getCompanyListUseCase = getCompanyListUseCase;
            _createCompanyUseCase = createCompanyUseCase;
            _updateCompanyUseCase = updateCompanyUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid company data.");

            try
            {
                var createdCompany = await _createCompanyUseCase
                    .ExecuteAsync(new CreateCompanyInput
                    {
                        CompanyName = request.Name,
                        Exchange = request.Exchange,
                        Isin = request.ISIN,
                        Ticker = request.Ticker,
                        Website = request.Website
                    }, cancellationToken);

                return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompany.Id }, createdCompany);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id, CancellationToken cancellationToken)
        {
            var company = await _getCompanyByIdUseCase.ExecuteAsync(id, cancellationToken);
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpGet("isin/{isin}")]
        public async Task<IActionResult> GetCompanyByIsin(string isin, CancellationToken cancellationToken)
        {
            var company = await _getCompanyByIsinUseCase.ExecuteAsync(isin, cancellationToken);
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyList(CancellationToken cancellationToken)
        {
            var companies = await _getCompanyListUseCase.ExecuteAsync(cancellationToken);
            return Ok(companies);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedCompany = await _updateCompanyUseCase.ExecuteAsync(new UpdateCompanyDetail
                {
                    Id = id,
                    CompanyName = request.Name,
                    Exchange = request.Exchange,
                    Isin = request.ISIN,
                    Ticker = request.Ticker,
                    Website = request.Website
                }, cancellationToken);

                if (updatedCompany == null)
                    return NotFound();

                return Ok(updatedCompany);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
