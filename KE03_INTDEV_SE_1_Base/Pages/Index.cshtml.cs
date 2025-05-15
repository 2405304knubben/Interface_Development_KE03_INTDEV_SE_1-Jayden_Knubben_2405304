using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerRepository _customerRepository;

        public IList<Customer> Customers { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            Customers = new List<Customer>();
        }

        public void OnGet()
        {            
            Customers = _customerRepository.GetAllCustomers().ToList();                            
            _logger.LogInformation($"getting all {Customers.Count} customers");
        }
    }
}
