using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Work20210608.Interfaces;
using Work20210608.Models;
using Work20210608.ViewModel;
using Work20210608.Wappers;

namespace Work20210608.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessionWapper _sessionWapper;
        private readonly IMemberService _memberService;
        private readonly IMessageService _messageService;

        public HomeController(ILogger<HomeController> logger, ISessionWapper sessionWapper, IMemberService memberService, IMessageService messageService)
        {
            _logger = logger;
            _memberService = memberService;
            _sessionWapper = sessionWapper;
            _messageService = messageService;
        }

        public IActionResult Index(string userName = "", int nowPage = 1)
        {
            MemberViewModel memberVM = _memberService.GetMember(_sessionWapper.MemberId);
            ViewBag.memberVM = memberVM;

            List<MessageViewModel> messageVMs = _messageService.GetMessages();

            //依用戶名搜尋
            if (String.IsNullOrEmpty(userName) != true) messageVMs = messageVMs.Where(messageVM => 
                messageVM.UserName == userName
            ).ToList();
            ViewBag.userName = userName;

            //分頁
            int perPage = 10;
            int totalPage = (int)Math.Max(Math.Ceiling((double)messageVMs.Count / perPage), 1);

            messageVMs = messageVMs.Skip((nowPage-1)*perPage).Take(perPage).ToList();

            ViewBag.perPage = perPage;
            ViewBag.totalPage = totalPage;
            ViewBag.nowPage = nowPage;

            ViewBag.messageVMs = messageVMs;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
