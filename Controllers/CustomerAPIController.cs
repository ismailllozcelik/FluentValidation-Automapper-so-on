using AutoMapper;
using FluentValidation;
using FluentValidationApp.Web.DTOs;
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
        private readonly IValidator<Customer> _customerValidator;
        private readonly IMapper _mapper;

        public CustomerAPIController(AppDbContext context, IValidator<Customer> customerValidator, IMapper mapper)
        {
            _context = context;
            _customerValidator = customerValidator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> GetCustomers()
        {
            List<Customer> customer = await _context.Customers.ToListAsync();
            return _mapper.Map<List<CustomerDTO>>(customer);

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
