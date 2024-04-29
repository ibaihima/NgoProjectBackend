namespace NgoProject.Models
{
	public class UserConstants
	{
		public static List<UserModel> Users = new List<UserModel>()
		{
			new UserModel(){UserName="Admin", Password="123", EmailAddress="Admin@gmail.com", Role="Admin", FirstName="Iba", LastName="Code"},
			new UserModel(){UserName="User", Password="123", EmailAddress="User@email.com", Role="User", FirstName="Leba", LastName="Code"},

		};
	}
}
