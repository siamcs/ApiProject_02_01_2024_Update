using ApiProject_02_01_2024.DTOs;
using ApiProject_02_01_2024.Services.BankService;
using ApiProject_02_01_2024.Services.CustomerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApiProject_02_01_2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
       
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
         
        }

        [HttpGet("DropDown")]
        public async Task<IActionResult> DropDown()
        {
            var res = await _customerService.DropDown();
            return Ok(res);
        }

        #region Next Customer Code 
        [HttpGet("GenerateNextCustomerCode")]
        public async Task<IActionResult> GenerateNextCustomerCode()
        {
            var nextCode = await _customerService.GenerateNextCustomerCode();
            return Ok(nextCode);
        }




        [HttpGet("GenerateNextCDACode")]
        public async Task<IActionResult> GenerateNextCDACode()
        {
            var nextCode = await _customerService.GenerateNextCustomerDeliveryAddressCode();
            return Ok(nextCode);
        }
        #endregion


       

        #region Get

        [HttpGet("{id:int?}")]
        public async Task<IActionResult> Index(int? id)
        {
            try
            {

                CustomerVM  customerVM = new CustomerVM();

                if (id.HasValue && id > 0)
                {
                    customerVM = await _customerService.GetByIdAsync(id.Value);
                    if (customerVM == null)
                    {
                        return NotFound();
                    }
                }

                return Ok(new { dataById = customerVM });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }




        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var customers = await _customerService.GetAllAsync();

                return Ok(new { AllData = customers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        #endregion


        #region Post
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CustomerVM customerVM)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _customerService.IsExistAsync(customerVM.CustomerName, customerVM.Id))
                {
                    return Ok(new { isSuccess = false, message = $"Already  Exists!", isDuplicate = true });
                }
                if (string.IsNullOrEmpty(customerVM.CustomerCode))
                {
                    customerVM.CustomerCode = await _customerService.GenerateNextCustomerCode();
                }


                var result = await _customerService.SaveAsync(customerVM);
                if (!result)
                {
                    return StatusCode(500, "Failed to save.");
                }

                return Ok("Saved Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, CustomerVM customerVM)
        {
            try
            {
                if (await _customerService.IsExistAsync(customerVM.CustomerName,customerVM.Id))
                {
                    return Ok(new { isSuccess = false, message = $"Already  Exists!", isDuplicate = true });
                }
                if (id != customerVM.Id)
                {
                    return BadRequest("");
                }

                var result = await _customerService.UpdateAsync(customerVM);
                if (!result)
                {
                    return StatusCode(500, "Failed to updat.");
                }

                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #region Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _customerService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound("Data not found for delete");
                }



                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        #endregion

        #endregion
    }
}
