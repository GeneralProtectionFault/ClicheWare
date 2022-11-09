using System.Collections.Generic;
using LiteDB;


public class Question
{
    public ObjectId _id { get; set; }

    public string MiniGame { get; set; }
    public int Difficulty { get; set; }
    public bool IsInPlay { get; set; }
    public string QuestionText { get; set; }
    /// bool portion of dictionary to return if answer is "correct" or not
    public List<KeyValuePair<string, bool>> Answers { get; set; }
}