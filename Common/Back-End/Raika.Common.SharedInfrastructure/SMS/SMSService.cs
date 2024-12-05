using RestSharp;

namespace Raika.Common.SharedInfrastructure.SMS
{
    public class SMSService : ISMSService
    {
        public int SendOTPToUser(string mobile, string code)
        {
            var client = new RestClient("https://api.sms.ir/v1/send/verify");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("x-api-key", "iLb3Pz9DOVJeC0CMBtdAWLPQm9nCWkhWUkTHBEo9Lverva2eTYwt14XTKVvTBfLE");
            request.AddHeader("Content-Type", "application/json");
            var body = "{\"mobile\": \"" + mobile + "\",\"templateId\": 607864,\"parameters\": [{\"name\": \"Code\",\"value\": \"" + code + "\"}]}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return ((int)response.StatusCode);
        }

        public int SendResetPasswordCodeToUser(string mobile, string code)
        {
            var client = new RestClient("https://api.sms.ir/v1/send/verify");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("x-api-key", "iLb3Pz9DOVJeC0CMBtdAWLPQm9nCWkhWUkTHBEo9Lverva2eTYwt14XTKVvTBfLE");
            request.AddHeader("Content-Type", "application/json");
            var body = "{\"mobile\": \"" + mobile + "\",\"templateId\": 192302,\"parameters\": [{\"name\": \"Code\",\"value\": \"" + code + "\"}]}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return ((int)response.StatusCode);
        }

        public int SendResetPasswordLinkToUser(string mobile, string guid)
        {
            var client = new RestClient("https://api.sms.ir/v1/send/verify");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("x-api-key", "iLb3Pz9DOVJeC0CMBtdAWLPQm9nCWkhWUkTHBEo9Lverva2eTYwt14XTKVvTBfLE");
            request.AddHeader("Content-Type", "application/json");
            var body = "{\"mobile\": \"" + mobile + "\",\"templateId\": 797155,\"parameters\": [{\"name\": \"GUID\",\"value\": \"" + guid + "\"},{\"name\":\"MOBILE\",\"value\":\"" + mobile + "\"}]}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return ((int)response.StatusCode);
        }
    }

}
