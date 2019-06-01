using System.Threading.Tasks;
using Infrastructure.Result;

namespace QuizRequestExtendedService.Database
{

    public interface IUserRepository<TToken, in TAuthData>
    {
        Task RegisterUser(TAuthData data);
        Task<Maybe<TToken>> AuthenticateUser(TAuthData data);
    }
}