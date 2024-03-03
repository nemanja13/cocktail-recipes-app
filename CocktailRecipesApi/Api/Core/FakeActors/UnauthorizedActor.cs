using Application.DataTransfer;
using Application;

namespace Api.Core.FakeActors
{
    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => 0;
        public string Identity => "Unauthorized Actor";
        public IEnumerable<int> AllowedUseCases => Enumerable.Range(7, 100);
        public UserDto User => new UserDto();
    }
}
