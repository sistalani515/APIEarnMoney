using APIEarnMoney.Models.Entities;
using APIEarnMoney.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIEarnMoney.Controllers
{
    public class EarnMoneyUserController : BaseController
    {
        private readonly IEarnMoneyService service;
        private readonly ILogger<EarnMoneyUserController> logger;

        public EarnMoneyUserController(IEarnMoneyService service, ILogger<EarnMoneyUserController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] int limit = 10)
        {
            try
            {
                var result = await service.GetAllUsers(limit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> InsertUser([FromQuery]string rawData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rawData)) return BadRequest("data invalid");

                var user = EarnMoneyUser.ParseFromRaw(rawData);
                //var result = await service.InsertUser(user);
                var result = await service.AutoInsertNewUser(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RefreshUser([FromQuery] int limit = 100)
        {
            try
            {
                await service.RefreshUserWD(limit);
                await Task.Delay(1000);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RawDoMission([FromQuery] string googleId)
        {
            try
            {
                await Task.Delay(1000);
                await service.RawDoAutoMission(googleId).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RawDoWithdraw([FromQuery] string noHp)
        {
            try
            {
                await Task.Delay(1000);
                await service.DoAutoWithDraw(noHp);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
