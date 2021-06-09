using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work20210608.Extensions;
using Work20210608.ViewModel;

namespace Work20210608.Wappers
{
    #region Session引用 https://ithelp.ithome.com.tw/articles/10194799
    public interface ISessionWapper
    {
        int MemberId { get; set; }
    }


    public class SessionWapper : ISessionWapper
    {
        private static readonly string _MemberId = "session.MemberId";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionWapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session;
            }
        }

        public int MemberId
        {
            get
            {
                return Session.GetObject<int>(_MemberId);
            }
            set
            {
                Session.SetObject(_MemberId, value);
            }
        }
    }
    #endregion
}
