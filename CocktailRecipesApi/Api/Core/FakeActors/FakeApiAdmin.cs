using Application.DataTransfer;
using Application;

namespace Api.Core.FakeActors
{
    public class FakeApiAdmin : IApplicationActor
    {
        public int Id => 1;

        public string Identity => "Fake Api Admin";

        public IEnumerable<int> AllowedUseCases => Enumerable.Range(1, 100);
        public UserDto User => new UserDto { Username = "Admin" };
    }
}
