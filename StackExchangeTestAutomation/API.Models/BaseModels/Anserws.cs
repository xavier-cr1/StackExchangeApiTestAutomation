namespace API.Models
{
    using API.Models.BaseModels;
    using System.Collections.Generic;

    public class Anserws
    {
        public Owner Owner { get; set; }
        public bool IsAccepted { get; set; }
        public int Score { get; set; }
        public int LastActivityDate { get; set; }
        public int CreationDate { get; set; }
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
    }
}
