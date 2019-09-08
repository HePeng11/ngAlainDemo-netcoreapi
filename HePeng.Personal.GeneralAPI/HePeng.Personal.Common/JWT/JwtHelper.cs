using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//

namespace Common.Jwt
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJWT(TokenModel tokenModel, string jWTSecretKey)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),//用户Id
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString(),ClaimValueTypes.Integer64)
            };
            //秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSecretKey));//key不能过短
            //签名方式
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: "HP.GeneralAPI",
                claims: claims, //声明集合
                expires: DateTime.UtcNow.AddHours(24),//过期时间
                signingCredentials: creds);
            //生成字符串
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        /// <summary>
        /// 解析令牌
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModel SerializeJWT(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            //过期时间
            if (jwtToken.ValidTo < DateTime.Now)
            {
                throw new SecurityTokenExpiredException("token已过期，请重新登录");
            }
            var tm = new TokenModel
            {
                Uid = int.Parse(jwtToken.Id)
            };
            return tm;
        }
    }

    /// <summary>
    /// claims 令牌信息
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Uid { get; set; }
    }

    public class JwtConsts
    {
        public const string RoleName = "user";
    }
}