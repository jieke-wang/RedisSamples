using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RedisSessionSample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SessionSampleController : ControllerBase
    {
        /// <summary>
        /// 保存和获取单条会话数据
        [HttpGet]
        public IActionResult SetAndGetValue(string key, string value)
        {
            HttpContext.Session.SetString(key, value);

            return Content($"Key: {key}, Value: {HttpContext.Session.GetString(key)}", "text/plain");
        }

        /// <summary>
        /// 获取会话中所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllValue()
        {
            List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
            foreach (var key in HttpContext.Session.Keys)
            {
                datas.Add(new KeyValuePair<string, string>(key, HttpContext.Session.GetString(key)));
            }

            return Ok(datas);
        }
    }
}
