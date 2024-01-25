using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDBContext _context;

        public CustomerController(CustomerDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        [HttpGet("{email}")]
        public IActionResult GetCustomerByEmail(string email)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.EmailId == email);
            if (customer == null)
            {
                return NotFound($"Customer with email {email} not found");
            }

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCustomerByEmail), new { email = customer.EmailId }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCustomer = _context.Customers.Find(id);
            if (existingCustomer == null)
            {
                return NotFound($"Customer with Id {id} not found");
            }

            existingCustomer.FirstName = updatedCustomer.FirstName;
            existingCustomer.MobileNumber = updatedCustomer.MobileNumber;
            existingCustomer.EmailId = updatedCustomer.EmailId;

            _context.SaveChanges();

            return Ok(existingCustomer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var existingCustomer = _context.Customers.Find(id);
            if (existingCustomer == null)
            {
                return NotFound($"Customer with Id {id} not found");
            }

            _context.Customers.Remove(existingCustomer);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
