using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaPay.PortWallet.Helpers;


namespace MetaPay.PortWallet.RequestHandler
{
    public class TokenRequest
    {


        public static string generateToken()
        {
            var appKey = AppSecret.AppKey;
            var secretKey = AppSecret.SecretKey;

            return HasGenerator.Base64Encode(appKey + ":" + HasGenerator.Md5HasGenerator(secretKey + HasGenerator.Timestamp()));
        }

    }
}
