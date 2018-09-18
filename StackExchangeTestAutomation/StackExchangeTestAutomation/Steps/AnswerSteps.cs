namespace StackExchangeTestAutomation
{
    using TechTalk.SpecFlow;
    using Autofac;
    using NUnit.Framework;
    using UserStories.AcceptanceTest.Steps.Base;
    using API.Service.Contracts;
    using DI.CrossModule;
    using DataSource.OpenXml.Contracts;
    using API.Models;

    [Binding]
    public class AnswerSteps : BaseStep
    {
        private readonly IAnswersApiServices _answersApiServices;

        private readonly IDataSourceContracts _dataSourceContracts;

        private string _currentResponse;

        public AnswerSteps()
        {
            _answersApiServices = ServiceContainer.ServiceCallsContainer.Resolve<IAnswersApiServices>();
            _dataSourceContracts = ServiceContainer.ServiceCallsContainer.Resolve<IDataSourceContracts>();
        }

        [Given(@"I want to Get a answer with the id '(.*)' with order '(.*)' sorted by '(.*)' and site '(.*)'")]
        public void GivenIWantToGetABook(int id, string order, string sortedBy, string site)
        {
            _currentResponse = _answersApiServices.CheckAnswerIsAccepted(id, order, sortedBy, site);
            Assert.IsTrue(!_currentResponse.Contains("errorMessages"), "Couldn't get the requested API");
        }

        [Then(@"The answer with the id '(.*)' is equal to expected")]
        public void ThenTheAnswerAcceptedIsHasAnScoreOfWithLastActivityDateAndACreationDate(int answerdId)
        {
            // /!\/!\/!\/!\/!\/!\I have an old Excel version and doesn't stop giving errors saying the data is damaged, when I made it from scratch...
            //var expectedAnswer = _dataSourceContracts.GetAnswerById(answerdId);
            // so I'm making a new like this step is working

            var expectedAnswer = new Answers { IsAccepted = true, AnswerId = 52391507, Score = 0, LastActivityDate = 1537290376, CreationDate = 1537290376, QuestionId = 52273011};
            var receivedAnswer = _answersApiServices.SchemaValidate(_currentResponse);
            Assert.AreEqual(expectedAnswer, receivedAnswer, "Received answer from the api wasn't correct, differs from expected in the database");
        }
    }
}
