namespace Sample.Application
{
    public static class UserExtension
    {
        public static bool IsPasswordConfirmation(this Sample.Models.User user)
        {
            return (user.Password == user.PasswordConfirmation) ? true : false;
        }
    }
}
