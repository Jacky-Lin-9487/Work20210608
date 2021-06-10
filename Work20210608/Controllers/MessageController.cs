using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work20210608.Interfaces;
using Work20210608.ViewModel;
using Work20210608.Wappers;

namespace Work20210608.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly ISessionWapper _sessionWapper;
        private readonly IMemberService _memberService;

        public MessageController(IMessageService messageService, ISessionWapper sessionWapper, IMemberService memberService)
        {
            _messageService = messageService;
            _sessionWapper = sessionWapper;
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LeaveAMessage()
        {
            MemberViewModel memberVM = _memberService.GetMember(_sessionWapper.MemberId);
            if (memberVM == null) return RedirectToAction("Index", "Home");
            ViewBag.memberVM = memberVM;

            return View();
        }

        [HttpPost]
        public IActionResult LeaveAMessage(MessageViewModel messageVM)
        {
            if (messageVM.Content == "")
            {
                ViewBag.ErrorMessage = "請輸入留言";
                return View();
            }

            messageVM.MemberId = _sessionWapper.MemberId;
            _messageService.CreateMessage(messageVM);

            return RedirectToAction("Index", "Home");
        }
    }
}
