namespace Raika.Common.SharedInfrastructure.SMS
{
    public interface ISMSService
    {
        int SendOTPToUser(string mobile, string code);
        int SendResetPasswordCodeToUser(string mobile, string code);
        int SendResetPasswordLinkToUser(string mobile, string guid);

    }
}
