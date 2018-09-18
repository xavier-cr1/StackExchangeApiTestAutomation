namespace API.Service.Calls
{
    using System;
    using API.Service.Contracts;
    using API.ServiceCalls;

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

        public bool HasAnswerExpectedValues(int answerdId, bool accepted, int score, int lastActivityDate, int creationDate)
        {
            throw new NotImplementedException();
        }
    }
}
