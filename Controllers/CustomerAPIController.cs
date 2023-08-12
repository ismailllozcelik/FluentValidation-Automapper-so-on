using FluentValidation;
using FluentValidationApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluentValidationApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Customer> _customerValidator ;

        public CustomerAPIController(AppDbContext context, IValidator<Customer> customerValidator)
        {
            _context = context; 
            _customerValidator = customerValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
             
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            var result = _customerValidator.Validate(customer);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(x => new { property = x.PropertyName, error = x.ErrorMessage }));
            }
            var savedCustomer = await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
            return Ok(savedCustomer);
        }

    }
}
