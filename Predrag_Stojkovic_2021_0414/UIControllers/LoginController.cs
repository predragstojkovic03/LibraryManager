using Client.Server;
using Infrastructure.Communication;
using Server.Dtos;

namespace Predrag_Stojkovic_2021_0414.UIControllers
{
  public class LoginController
  {
    private readonly ServerAdapter _serverAdapter;

    public LoginController(ServerAdapter serverAdapter)
    {
      _serverAdapter = serverAdapter;
    }

    public Response? Login(string username, string password)
    {
      var dto = new AuthLoginDto { Username = username, Password = password };
      return _serverAdapter.MakeRequest(Operation.AuthLogin, dto);
    }
  }
}
