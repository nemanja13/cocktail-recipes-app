using Application.DataTransfer;
using Application;

namespace Api.Core.Jwt
{
    public class JwtActor : IApplicationActor
    {
        public int Id { get; set; }
        public string Identity { get; set; }
        public IEnumerable<int> AllowedUseCases { get; set; }
        public UserDto User { get; set; }
    }
}
