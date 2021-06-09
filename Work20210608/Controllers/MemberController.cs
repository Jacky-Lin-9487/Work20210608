using Microsoft.AspNetCore.Http;
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
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly ISessionWapper _sessionWapper;

        public MemberController(IMemberService memberService, ISessionWapper sessionWapper)
        {
            _memberService = memberService;
            _sessionWapper = sessionWapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(MemberViewModel memberVM)
        {
            if (memberVM.Name == "" || memberVM.Password == "")
            {
                ViewBag.ErrorMessage = "帳號或密碼不可為空";
                return View();
            }

            memberVM = _memberService.Login(memberVM);

            if (memberVM == null)
            {
                ViewBag.ErrorMessage = "帳號或密碼不存在";
                return View();
            }
            else _sessionWapper.MemberId = memberVM.MemberId;

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(MemberViewModel memberVM, IFormCollection formCollection)
        {
            if(memberVM.Name == "" || memberVM.Password == "")
            {
                ViewBag.ErrorMessage = "帳號或密碼不可為空";
                return View();
            }

            if (memberVM.Password != formCollection["checkPassword"])
            {
                ViewBag.ErrorMessage = "密碼與確認密碼不相符";
                return View();
            }
            
            _memberService.Register(memberVM);

            return RedirectToAction("Login", "Member", new { memberVM = memberVM });
        }

        public IActionResult Logout()
        {
            _sessionWapper.MemberId = 0;

            return RedirectToAction("Index", "Home");
        }
    }
}
