using ProjectTest.Configurations;
using ProjectTest.Data.VO;
using ProjectTest.Repository;
using ProjectTest.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProjectTest.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _cofiguration;

        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration cofiguration, IUserRepository repository, ITokenService tokenService)
        {
            _cofiguration = cofiguration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredential)
        {
            var user = _repository.ValidateCredentials(userCredential);

            if (user == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var accessToken = _tokenService.GerenateAccessToken(claims);
            var refreshToken = _tokenService.GerenateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_cofiguration.DaysToExpiry);

            _repository.RefreshUserInfo(user);

            DateTime createTime = DateTime.Now;
            DateTime expirationData = createTime.AddMinutes(_cofiguration.Minutes);

            return new TokenVO(
                true,
                createTime.ToString(DATE_FORMAT),
                expirationData.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var username = principal.Identity.Name;
            var user = _repository.ValidateCredentials(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return null;


            accessToken = _tokenService.GerenateAccessToken(principal.Claims);
            refreshToken = _tokenService.GerenateRefreshToken();

            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);

            DateTime createTime = DateTime.Now;
            DateTime expirationData = createTime.AddMinutes(_cofiguration.Minutes);

            return new TokenVO(
                true,
                createTime.ToString(DATE_FORMAT),
                expirationData.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
