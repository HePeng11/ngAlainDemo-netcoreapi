using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace HePeng.Personal.Common.JWT
{
    public class UserPrincipal : ClaimsPrincipal
    {
        public UserPrincipal(ClaimsIdentity claimsIdentity,int id) : base(claimsIdentity)
        {
            Id = id;
        }

        public int Id { get; }
    }

}
