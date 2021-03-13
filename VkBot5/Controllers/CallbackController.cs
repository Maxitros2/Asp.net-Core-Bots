using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot5.Models;
using VkNet.Abstractions;
using VkNet.Model;

namespace VkBot5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IVkApi vkApi;
        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.vkApi = vkApi;
        }
        [HttpPost]
        public IActionResult Callback([FromBody] IncomeMessage incomeMessage)
        {
            switch (incomeMessage.Type)
            {
                case "confirmation":
                    return Ok(configuration["Config:ConfirmMessage"]);
                case "message_new":
                    {
                        var msg = Message.FromJson(new VkNet.Utils.VkResponse(incomeMessage.Object));
                        vkApi.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                        {
                            RandomId = new DateTime().Millisecond,
                            PeerId = msg.PeerId.Value,
                            Message = "соси хуй"
                        });
                        break;                       
                    }
                case "message_typing_state":
                    {
                        long msg = incomeMessage.Object.Value<long>("from_id");
                        vkApi.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                        {
                            RandomId = new DateTime().Millisecond,
                            PeerId = msg,
                            Message = "че пишешь хуила"
                        });
                        break;
                    }
            }
            return Ok("ok");
        }
    }
}
