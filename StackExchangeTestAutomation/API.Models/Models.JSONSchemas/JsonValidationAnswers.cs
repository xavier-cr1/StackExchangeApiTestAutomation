namespace API.Models.Models.JSONSchemas
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;

    public static class JsonValidationAnswers
    {
        //this could be more generic for other models usages
        public static Answers IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    JToken jAnswer = obj["items"];
                    var answer = new Answers()
                    {
                        IsAccepted = (bool)jAnswer["is_accepted"],
                        LastActivityDate = (int)jAnswer["last_activity_date"],
                        CreationDate = (int)jAnswer["creation_date"],
                        QuestionId = (int)jAnswer["question_id"],
                        AnswerId = (int)jAnswer["answer_id"],
                        Score = (int)jAnswer["score"]
                    };
                    return answer;
                }
                catch (JsonReaderException jex)
                {
                    Console.WriteLine(jex.Message);
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
