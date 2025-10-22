using Client.Server;
using Infrastructure.Communication;


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
  var loginRequest = new { Username = username, Password = password };
  return _serverAdapter.MakeRequest(Operation.AuthLogin, loginRequest);
    }
  }
}
