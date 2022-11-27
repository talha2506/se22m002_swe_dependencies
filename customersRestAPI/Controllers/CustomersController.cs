using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using customersRestAPI.Entities;
using customersRestAPI.DTOs;

namespace customersRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomersController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/basicInfo
        [HttpGet("basicInfo")]
        public async Task<ActionResult<IEnumerable<CustomerBasicDTO>>> GetCustomersBasicInfoOnly()
        {
            return await _context.Customers
                .Select(x => CustomerToCustomerBasicDTO(x))
                .ToListAsync();
        }

        // GET: api/Customers/withSumOfBalance
        [HttpGet("withSumOfBalance")]
        public async Task<ActionResult<IEnumerable<CustomerWithSumOfBalanceDTO>>> GetCustomersWithSumOfBalance()
        {
            return await _context.Customers
                .Select(x => CustomerToCustomerWithSumOfBalanceDTO(x))
                .ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(long id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        private static CustomerBasicDTO CustomerToCustomerBasicDTO(Customer customer)
        {
            return new CustomerBasicDTO
            {
                Name = customer.Name,
                EmailAddress = customer.EmailAddress,
                Status = customer.Status
            };
        }

        private static CustomerWithSumOfBalanceDTO CustomerToCustomerWithSumOfBalanceDTO(Customer customer)
        {
            var aggregatedBalance = 0d;

            if (customer.FinancialProducts != null)
            {
                foreach (FinancialProduct product in customer.FinancialProducts)
                {
                    aggregatedBalance += product.Balance;
                }
            }

            return new CustomerWithSumOfBalanceDTO
            {
                Name = customer.Name,
                AggregatedBalance = aggregatedBalance
            };
        }
    }
}
