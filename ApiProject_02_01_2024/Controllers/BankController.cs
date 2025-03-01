using ApiProject_02_01_2024.DTOs;
using ApiProject_02_01_2024.Models;
using ApiProject_02_01_2024.Services.BankService;
using ApiProject_02_01_2024.Services.CommonService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject_02_01_2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {

        private readonly IBankService _bankService;
       
        public BankController(IBankService bankService)
        {
            _bankService = bankService;
            //this.commonService = commonService;
        }



        #region Get

        [HttpGet("{id:int?}")]
        public async Task<IActionResult> Index(int? id)
        {
            try
            {

                BankVM bankVM = new BankVM();

                if (id.HasValue && id > 0)
                {
                    bankVM = await _bankService.GetByIdAsync(id.Value);
                    if (bankVM == null)
                    {
                        return NotFound();
                    }
                }

                return Ok(new { SelectedBank = bankVM});
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
                var bankList = await _bankService.GetAllAsync();

                return Ok(new { AllBanks = bankList  });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        #endregion


        #region Post
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BankVM bankVM)
        {
            try
            {

                if(await _bankService.IsExistAsync(bankVM.BankName))
                {
                    return Ok(new { isSuccess = false, message = $"Already  Exists!", isDuplicate = true });
                }

                if (string.IsNullOrEmpty(bankVM.BankCode))
                {
                    bankVM.BankCode = await _bankService.GenerateNextBankCodeAsync();
                }


                if (bankVM.Id == 0)
                {
                    var result = await _bankService.SaveAsync(bankVM);
                    if (!result)
                    {
                        return StatusCode(500, "Failed to save the bank.");
                    }
                    return Ok("Saved Successfully");
                }
                else
                {
                   
                    return NotFound();
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BankVM bankVM)
        {
            try
            {
                if (id != bankVM.Id)
                {
                    return BadRequest("Bank ID mismatch");
                }

                var result = await _bankService.UpdateAsync(bankVM);
                if (!result)
                {
                    return StatusCode(500, "Failed to update the bank.");
                }

                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion


        #region Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Console.WriteLine($"Received ID: {id}"); // Debugging
                var result = await _bankService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                

                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        #endregion


        #region Delete multiple
        
        [HttpPost("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            
            try
            {
                foreach (var id in ids)
                {
                    try
                    {
                        var result = await _bankService.DeleteAsync(id);
                        if (!result)
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, ex.Message);

                    }
                }

                return Ok("Datum Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region Next Bank Code 
        [HttpGet("GenerateNextBankCode")]
        public async Task<IActionResult> GenerateNextBankCode()
        {
            var nextCode = await _bankService.GenerateNextBankCodeAsync();
            return Ok(nextCode);
        }

        #endregion

      


       

    }
}
