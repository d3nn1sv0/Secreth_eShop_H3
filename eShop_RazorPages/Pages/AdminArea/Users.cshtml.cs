using System.Collections.Generic;
using System.Threading.Tasks;
using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Core.Types;

public class UsersModel : PageModel
{
    private readonly IRepository<Customer> _customerRepository;

    public UsersModel(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IEnumerable<Customer> Customers { get; set; }

    public async Task OnGetAsync(bool showHiddenUsers = false)
    {
        var allCustomers = await _customerRepository.GetAllAsync();
        if (showHiddenUsers)
        {
            Customers = allCustomers.ToList();
        }
        else
        {
            Customers = allCustomers.Where(c => c.IsVisible).ToList();
        }
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        int customerId = Convert.ToInt32(Request.Form["customerId"]);
        if (customerId == 0)
        {
            return NotFound();
        }

        var customer = await _customerRepository.GetByIdAsync(customerId);

        if (customer != null)
        {
            customer.IsVisible = false;
            await _customerRepository.UpdateAsync(customer);
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostActivateAsync()
    {
        int customerId = Convert.ToInt32(Request.Form["customerId"]);
        if (customerId == 0)
        {
            return NotFound();
        }

        var customer = await _customerRepository.GetByIdAsync(customerId);

        if (customer != null)
        {
            customer.IsVisible = true;
            await _customerRepository.UpdateAsync(customer);
        }

        return RedirectToPage();
    }

}

