namespace App.Application.Dtos
{
   internal record GetUserDto(int Id,string UserName,DateOnly DateOfBirth);

   public record UserAddressDto(string Pincode);

   // public record CreateUSerDto(string UserName, string Password, UserAddressDto UserAddressDto);


}