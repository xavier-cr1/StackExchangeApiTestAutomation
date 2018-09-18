namespace API.Service.Contracts
{
    using API.Models;

    public interface IAnswersApiServices
    {
        string CheckAnswerIsAccepted(int answerId, string order, string sort, string site);

        Answers SchemaValidate(string currentResponse);
    }
}
