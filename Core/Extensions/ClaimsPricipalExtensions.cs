using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Core.Extensions;


namespace Core.Extensions
{
    public static class ClaimsPricipalExtensions
    {
        //bu extension sayesinde role de başka cliamlere de erişebilmeliyim. ileride id sine de ulaşacak bir şekilde kod yazıyoruz
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)//hangi claimType için filtrelemek yapacağız?
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();//claimPrincipal var mı? claimType var mı? listele
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }

        //autorizasyon için aspect altyapısını yapalım
    }
}
