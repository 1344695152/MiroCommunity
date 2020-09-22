using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MiroCommunity_API.Controllers
{
    /// <summary>
    /// 登录接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtConfig config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public LoginController(IOptions<JwtConfig> options)
        {
            config = options.Value;
        }
    }
}