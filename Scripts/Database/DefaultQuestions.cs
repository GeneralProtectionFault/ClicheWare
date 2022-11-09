using Godot;
using System.Collections.Generic;

public partial class DefaultQuestions : Node
{
    public override void _Ready()
    {
        var question = new Question 
        {
            MiniGame = "Bed",
            Difficulty = 1,
            IsInPlay = true,
            QuestionText = "2 + 2",
            Answers = new List<KeyValuePair<string, bool>>
            {
                new KeyValuePair<string, bool>("4", false),
                new KeyValuePair<string, bool>("5", true)
            }
        };

        DatabaseHandler.QuestionCollection.Insert(question);
    }
}