using Microsoft.AspNetCore.Mvc;

namespace WebAPI;

[ApiController]
[Route("api/[controller]")]
public class SmsCodeRequestsController : ControllerBase
{

    private readonly IConfiguration _config;
    private readonly ILogger<SmsCodeRequestsController> _logger;

    private string[] fromMobiles = { "+15017122661", "+15017125555", "+15017123333" };

    public SmsCodeRequestsController(ILogger<SmsCodeRequestsController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _config = configuration;
    }


    [HttpGet(Name = "GetSmsCodeRequests")]
    public SmsCodeListResponseDto Get()
    {
        SmsCodeRequestModel m = new SmsCodeRequestModel(_config);
        SmsCodeListResponseDto response = m.getCodeList(0, 10);
        return response;
    }
}
