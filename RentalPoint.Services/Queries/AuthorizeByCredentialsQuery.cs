using MediatR;
using RentalPoint.Data.Entities;

namespace RentalPoint.Services.Queries
{
    public class AuthorizeByCredentialsQuery : IRequest<User>
    {
        public AuthorizeByCredentialsQuery(User user, bool afterRegister = false)
        {
            User = user;
            AfterRegister = afterRegister;
        }
        
        public bool AfterRegister { get; }
        public User User { get; }
    }
}