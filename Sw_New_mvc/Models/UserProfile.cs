using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace Sw_New_mvc.Models
{
    public class UserProfile : ProfileBase
    {
        [SettingsAllowAnonymous(false)]
        public int Profile_ID{ get; set; }
    }
}