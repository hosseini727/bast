namespace Raika.Common.SharedApplicationServices.Exceptions
{
    public class ApplicationExceptionMessages
    {
        public static string EntityNotFoundError() => "Data not found.";
        public static string UpdateEntityError() => "Update the entity has failed.";
        public static string DeleteEntityError() => "Dalete the entity has failed.";
        public static string SearchResultsEmpty() => "Search has no result.";
        public static string ValidationError() => "Some data validation has failed.";
        public static string AddEntityError() => "Insert new entity failed.";
        public static string DataAllreadyExist() => "Duplicate data.";
        public static string UserRegisterFailure() => "User sign up failed.";
        public static string UserLoginFailure() => "User sign in failed.";
        public static string UserRegistrationNotCompeleted() => "Registration for this user has not compeleted yet.";
        public static string WrongPassword() => "Password is incorrect.";
        public static string ConfirmationCodeUsageTimeExpired() => "Time for using confirmation code has expired.";
        public static string WrongConfirmationCode() => "The confirmation code is incorrect.";
        public static string WrongInformation() => "Data is not correct.";
        public static string TokenCreationException() => "Create token failes.";
        public static string GeneralError() => "General failure occurred.";
    }
}
