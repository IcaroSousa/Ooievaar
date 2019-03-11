using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ooievaar.Common;
using Ooievaar.Common.Classes;
using Ooievaar.Common.Interfaces;

namespace Ooievaar.Server.Controllers
{
    [EnableCors("AllowAll")]
    [Route("/")]
    [ApiController]
    public class OoievaarController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IRedisQueue _redisQueue;

        public OoievaarController(IConfiguration pConfig)
        {
            _config = pConfig;
            _redisQueue = new RedisQueue(_config);
        }

        [HttpGet]
        [Route("/Ping")]
        public JsonResult Ping() 
        {
            return Json("Pong...");
        }

        [HttpPost]
        [Route("/EnqueueItem")]
        public void EnqueueItem([FromBody] ReleaseDTO pItem) 
        {
            string releaseJson = JsonConvert.SerializeObject(pItem);
            _redisQueue.Enqueue(releaseJson);
        }

        [HttpGet]
        [Route("/DequeueItem")]
        public JsonResult DequeueItem() 
        {
            string releaseJson = _redisQueue.Dequeue();
            if (string.IsNullOrWhiteSpace(releaseJson))
                return Json(string.Empty);

            ReleaseDTO release = JsonConvert.DeserializeObject<ReleaseDTO>(releaseJson);
            return Json(release);
        }

        [HttpPost]
        [Route("/RemoveFromQueue")]
        public void RemoveFromQueue([FromBody] ReleaseDTO pRelease)
        {
            string releaseJson = JsonConvert.SerializeObject(pRelease);
            _redisQueue.DeleteItem(releaseJson);
        }

        [HttpGet]
        [Route("/GetListFromQueue")]
        public JsonResult GetListFromQueue() 
        {
            IList<string> itens = _redisQueue.GetListFromQueue();
            return Json(itens);
        }

    }
}
