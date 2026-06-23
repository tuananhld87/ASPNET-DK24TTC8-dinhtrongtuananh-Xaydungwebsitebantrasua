using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopOnline.Common
{
    public class UserLogin
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string GroupId { set; get; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + ' ' + LastName;
    }
}
