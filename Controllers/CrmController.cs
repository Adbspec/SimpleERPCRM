using ERP.AppCode;
using ERP.Dto;
using ERP.Models;
using iText.Commons.Actions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ERP.Controllers
{
    [Authorize]
    public class CrmController : Controller
    {
        private readonly DBContext _dBContext;

        public CrmController(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

       

        // *****************************************************************************************
        // *************************************** CUSTOMERS SECTION ************************************
        // *****************************************************************************************

        public async Task<IActionResult> ViewCustomer(int? CustomerId)
        {
          
            if (CustomerId.HasValue)
            {
                var customerInteractions = await _dBContext.Customerinteractions
                    .Where(c => c.CustomerId == CustomerId.Value).OrderBy(c=>c.Timestamp).ToListAsync();

                var customerData = await _dBContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == CustomerId);

                ViewData["CustomerId"] = CustomerId;
                @ViewData["CustomerName"] = $"{customerData.FirstName} {customerData.LastName}";
                return View("CustomerInteractions", customerInteractions); // Assuming you have a view for a single customer
            }

            // If no CustomerId is provided, fetch all customers
            IEnumerable<Customer> customers = await _dBContext.Customers.ToListAsync();
            return View("ViewCustomerList", customers); // Assuming you have a view for the list of customers
        }



        [HttpPost]
        public async Task<IActionResult> AddCustomerInfo(Customer customerDTO)
        {
            if (!ModelState.IsValid)
            {
                // Return validation errors as JSON
                return Json(new { success = false, message = "Form validation failed." });
            }

            try
            {
                // Check if the customer already exists based on Email or Phone
                var existingCustomer = await _dBContext.Customers
                    .FirstOrDefaultAsync(c => c.Email == customerDTO.Email || c.Phone == customerDTO.Phone);

                if (existingCustomer != null)
                {
                    // Return an error if duplicate entry is found
                    return Json(new { success = false, message = "A customer with the same email or phone number already exists." });
                }
                var customer = new Customer
                {
                    FirstName = customerDTO.FirstName,
                    LastName = customerDTO.LastName,
                    Email = customerDTO.Email,
                    Phone = customerDTO.Phone,
                    Address = customerDTO.Address,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _dBContext.Customers.Add(customer);
                await _dBContext.SaveChangesAsync();

                // Return success response as JSON
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the exception (optional: can be done via a logger)
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");

                // Return error response as JSON
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCustomerInfo(Customer customerDTO)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, return the view with customerDTO for feedback
                return View(customerDTO);
            }

            try
            {
                var customer = await _dBContext.Customers.FindAsync(customerDTO.CustomerId);
                if (customer == null)
                {
                    return NotFound();
                }

                // Update customer properties
                customer.FirstName = customerDTO.FirstName;
                customer.LastName = customerDTO.LastName;
                customer.Email = customerDTO.Email;
                customer.Phone = customerDTO.Phone;
                customer.Address = customerDTO.Address;
                customer.UpdatedAt = DateTime.UtcNow;

                // Save changes to the database
                await _dBContext.SaveChangesAsync();

                return Json(new { success = true }); // Return success response
            }
            catch (Exception ex)
            {
                // Log the exception (optional: can be done via a logger)
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return Json(new { success = false, message = "An error occurred while updating customer." });
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int CustomerId)
        {
            try
            {
                // Replace this with actual logic to delete the customer
                var customer = _dBContext.Customers.Find(CustomerId);
                if (customer != null)
                {
                    _dBContext.Customers.Remove(customer);
                    await _dBContext.SaveChangesAsync();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Customer not found." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddCustomerInteraction(CustomerInteractionsDto customerInteractionsDto)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                // Return the validation errors if the model is not valid
                return View(customerInteractionsDto); // Or return a JSON response if you're using AJAX
            }

            try
            {
                // Create the Customerinteraction object
                var customerInteraction = new Customerinteraction
                {
                    CustomerId = customerInteractionsDto.CustomerId,
                    InteractionType = customerInteractionsDto.InteractionType,
                    Details = customerInteractionsDto.Details,
                    Timestamp = DateTime.UtcNow // Manually generate the Timestamp
                };

                // Add the new record to the database
                _dBContext.Customerinteractions.Add(customerInteraction);
                await _dBContext.SaveChangesAsync();

                // Redirect to the referring page (or wherever you need to go after adding the record)
                return Redirect(Request.Headers["Referer"].ToString());
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a generic error message or log it as needed
                ModelState.AddModelError("", "An error occurred while saving the interaction. Please try again later.");
                return View(customerInteractionsDto); // Or return a JSON response
            }
        }
        // *****************************************************************************************
        // *************************************** LEADS SECTION ************************************
        // *****************************************************************************************

        public async Task<IActionResult> ViewLeads()
        {
            IEnumerable<Lead> leads = await _dBContext.Leads.ToListAsync();
            return View(leads);
        }


        [HttpPost]
        public async Task<IActionResult> AddLeadsInfo(LeadDto leadDto)
        {
            if (!ModelState.IsValid)
            {
                // Return validation errors as JSON
                return Json(new { success = false, message = "Form validation failed." });
            }

            try
            {
                // Check for duplicate leads by email or phone number
                var existingLead = await _dBContext.Leads
                    .FirstOrDefaultAsync(l => l.Email == leadDto.Email || l.PhoneNumber == leadDto.PhoneNumber);

                if (existingLead != null)
                {
                    return Json(new { success = false, message = "A lead with the same email or phone number already exists." });
                }

                // Map LeadDto to the database entity
                var lead = new Lead
                {
                    Name = leadDto.Name,
                    Email = leadDto.Email,
                    PhoneNumber = leadDto.PhoneNumber,
                    Status = leadDto.Status
                };

                // Add and save the lead
                _dBContext.Leads.Add(lead);
                await _dBContext.SaveChangesAsync();

                // Return success response
                return Json(new { success = true, message = "Lead added successfully." });
            }
            catch (Exception ex)
            {
                // Log and handle the exception
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }



        // *****************************************************************************************
        // *************************************** EMPLOYEE SECTION ************************************
        // *****************************************************************************************

        public async Task<IActionResult> ViewEmployees()
        {
            IEnumerable<Employee> employees = await _dBContext.Employees.ToListAsync();
            return View(employees);
        }


        public async Task<IActionResult> AddEmployee(EmployeeDto employeeDto)
        {

            if (!ModelState.IsValid)
            {
                // Return validation errors as JSON if the model state is invalid
                return Json(new { success = false, message = "Form validation failed." });
            }

            try
            {
                // Check for duplicate email (optional)
                var existingEmployee = await _dBContext.Employees
                    .FirstOrDefaultAsync(e => e.Email == employeeDto.Email);

                if (existingEmployee != null)
                {
                    return Json(new { success = false, message = "An employee with this email already exists." });
                }

                // Map EmployeeDto to Employee model
                var employee = new Employee
                {
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Email = employeeDto.Email,
                    Role = employeeDto.Role,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Add employee to the database
                _dBContext.Employees.Add(employee);
                await _dBContext.SaveChangesAsync();


                //Create a user of employee as well
                string hashPassword = MiscFunctions.HashedPassword("1234"); // Default password is 1234 for now in future this will be changed
                User user = new User
                {
                    UserId = "N.A",
                    UserEmail = employee.Email,
                    Username = employee.FirstName,
                    Role = employee.Role,
                    PasswordHash = hashPassword,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dBContext.Users.AddAsync(user);
                await _dBContext.SaveChangesAsync();
                // Return success response
                return Json(new { success = true, message = "Employee added successfully." });
            }
            catch (Exception ex)
            {
                // Log and handle any exceptions
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
