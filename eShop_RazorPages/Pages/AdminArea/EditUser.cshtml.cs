using System.Threading.Tasks;
using eShop_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class EditUserModel : PageModel
{
    private readonly IRepository<Customer> _customerRepository;

    [BindProperty]
    public Customer Customer { get; set; }

    public EditUserModel(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Customer = await _customerRepository.GetByIdAsync(id.Value);

        if (Customer == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _customerRepository.UpdateAsync(Customer);

        return RedirectToPage("./Users");
    }
}
