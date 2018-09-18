namespace DataSource.OpenXml.Contracts
{
    using API.Models;

    public interface IDataSourceContracts
    {
        Answers GetAnswerById(int answerId);
    }
}
