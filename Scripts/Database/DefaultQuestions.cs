using Godot;
using System.Collections.Generic;

public partial class DefaultQuestions : Node
{
    public override void _Ready()
    {
        List<Question> QuestionList = new List<Question>()
        {
            new Question 
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
            },

            new Question
            {
                MiniGame = "Bed",
                Difficulty = 1,
                IsInPlay = true,
                QuestionText = "Pointed Ears",
                Answers = new List<KeyValuePair<string, bool>>
                {
                    new KeyValuePair<string, bool>("Cat", false),
                    new KeyValuePair<string, bool>("Mouse", true)
                }
            }
        };


        foreach(var Quest in QuestionList)
            DatabaseHandler.QuestionCollection.Insert(Quest);
    }
}