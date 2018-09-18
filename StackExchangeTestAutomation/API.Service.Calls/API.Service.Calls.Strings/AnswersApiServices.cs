namespace API.Service.Calls
{
    using System;
    using API.Models;
    using API.Service.Contracts;
    using API.ServiceCalls;
    using API.Models.Models.JSONSchemas;

    public class AnswersApiServices : BaseRequestService, IAnswersApiServices
    {
        private const string answersController = "answers";

        public string CheckAnswerIsAccepted(int answerId, string order, string sort, string site)
        {
            string value = $"/{answerId}";
            string paramaters = $"?order={order}&sort={sort}&site={site}";
            string answerRequest = MakeRequest(answersController, value, paramaters, "GET");

            return answerRequest;
        }

        public Answers SchemaValidate(string currentResponse)
        {
            return JsonValidationAnswers.IsValidJson(currentResponse);
        }
    }
}
