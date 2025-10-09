using ProjectTest.Data.VO;
using ProjectTest.Model;

namespace ProjectTest.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string userName);
        User RefreshUserInfo(User user);
        bool RevokeToken(string userName);

    }
}
