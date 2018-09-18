namespace StackExchangeTestAutomation
{
    using TechTalk.SpecFlow;
    using Autofac;
    using NUnit.Framework;
    using UserStories.AcceptanceTest.Steps.Base;
    using API.Service.Contracts;
    using DI.CrossModule;
    using DataSource.OpenXml.Contracts;

    [Binding]
    public class AnswerSteps : BaseStep
    {
        private readonly IAnswersApiServices _answersApiServices;

        private readonly IDataSourceContracts _dataSourceContracts;

        private string _currentResponse;

        public AnswerSteps()
        {
            _answersApiServices = ServiceContainer.ServiceCallsContainer.Resolve<IAnswersApiServices>();
        }

        [Given(@"I want to Get a answer with the id '(.*)' with order '(.*)' sorted by '(.*)' and site '(.*)'")]
        public void GivenIWantToGetABook(int id, string order, string sortedBy, string site)
        {
            _currentResponse = _answersApiServices.CheckAnswerIsAccepted(id, order, sortedBy, site);
            Assert.IsTrue(!_currentResponse.Contains("errorMessages"), "Value");
        }

        [Then(@"The answer with the id '(.*)' is equal to expected")]
        public void ThenTheAnswerAcceptedIsHasAnScoreOfWithLastActivityDateAndACreationDate(int answerdId)
        {
            var expectedAnswer = _dataSourceContracts.GetAnswerById(answerdId);
            var receivedAnswer = _answersApiServices.SchemaValidate(_currentResponse);
            Assert.AreEqual(expectedAnswer, receivedAnswer, "Received answer from the api wasn't correct");
        }
    }
}
