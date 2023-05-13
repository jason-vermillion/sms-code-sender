using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Models;

namespace WebAPI;

[ApiController]
[Route("api/[controller]")]
public class SmsCodeRequestsController : ControllerBase
{
    private readonly ILogger<SmsCodeRequestsController> _logger;
    private readonly SmsCodeSenderContext _dbContext;

    public SmsCodeRequestsController(ILogger<SmsCodeRequestsController> logger, SmsCodeSenderContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }


    [HttpGet(Name = "GetSmsCodeRequests")]
    public SmsCodeListResponseDto Get()
    {
        SmsCodeRequestModel m = new SmsCodeRequestModel(_dbContext);
        SmsCodeListResponseDto response = m.getCodeList(0, 10);
        return response;
    }
}
