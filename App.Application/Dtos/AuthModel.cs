namespace App.Application.Dtos
{
    internal record AuthResponseModel(string UserName, string Token);
    internal record AuthUser(int Id,string UserName);
}
