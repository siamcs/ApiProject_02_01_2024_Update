using ApiProject_02_01_2024.DTOs;

using ApiProject_02_01_2024.Services.CustomerTypeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject_02_01_2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypeController : ControllerBase
    {
        private readonly ICustomerTypeService customerTypeService;

        public CustomerTypeController(ICustomerTypeService customerTypeService)
        {
            this.customerTypeService = customerTypeService;
        }

        #region Get

        [HttpGet("DropDown")]
        public async Task<IActionResult> DropDown()
        {
            var res = await customerTypeService.DropSelection();
            return Ok(res);
        }

        #region Next Customer Code 
        [HttpGet("GenerateNextCusTypeCode")]
        public async Task<IActionResult> GenerateNextCusTypeCodeAsync()
        {
            var nextCode = await customerTypeService.GenerateNextCusTypeCodeAsync();
            return Ok(nextCode);
        }

        #endregion

        [HttpGet("{id:int?}")]
        public async Task<IActionResult> Index(int? id)
        {
            try
            {

                CustomerTypeVM designationVM = new CustomerTypeVM();

                if (id.HasValue && id > 0)
                {
                    designationVM = await customerTypeService.GetByIdAsync(id.Value);
                    if (designationVM == null)
                    {
                        return NotFound();
                    }
                }

                return Ok(new { dataById = designationVM });
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
                var designations = await customerTypeService.GetAllAsync();

                return Ok(new { AllData = designations });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        #endregion


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CustomerTypeVM  customerTypeVM)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await customerTypeService.IsExistAsync(customerTypeVM.CustomerTypeName, customerTypeVM.Id))
                {
                    return Ok(new { isSuccess = false, message = $"Already  Exists!", isDuplicate = true });
                }
                if (string.IsNullOrEmpty(customerTypeVM.CusTypeCode))
                {
                    customerTypeVM.CusTypeCode = await customerTypeService.GenerateNextCusTypeCodeAsync();
                }


                var result = await customerTypeService.SaveAsync(customerTypeVM);
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
        public async Task<IActionResult> Update(int id, CustomerTypeVM  customerTypeVM)
        {
            try
            {
                if (await customerTypeService.IsExistAsync(customerTypeVM.CustomerTypeName, customerTypeVM.Id))
                {
                    return Ok(new { isSuccess = false, message = $"Already  Exists!", isDuplicate = true });
                }
                if (id != customerTypeVM.Id)
                {
                    return BadRequest("");
                }

                var result = await customerTypeService.UpdateAsync(customerTypeVM);
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
                var result = await customerTypeService.DeleteAsync(id);
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

        [HttpPost("DuplicateCheck")]
        public async Task<IActionResult> DuplicateCheck(string name, int code)
        {
            if (await customerTypeService.IsExistAsync(name, code))
            {
                return Ok(new { isSuccess = true, message = "Already Exists!" });
            }

            return Ok(new { isSuccess = false });
        }

    }
}
