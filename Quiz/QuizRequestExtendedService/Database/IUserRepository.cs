using System.Threading.Tasks;
using Infrastructure.Result;

namespace QuizRequestExtendedService.Database
{

    public interface IUserRepository<TToken, in TAuthData>
    {
        Task RegisterUserAsync(TAuthData data);
        Task<Maybe<TToken>> AuthenticateUserAsync(TAuthData data);
        None RegisterUser(TAuthData data);

        Maybe<TToken> AuthenticateUser(TAuthData data);
    }
}