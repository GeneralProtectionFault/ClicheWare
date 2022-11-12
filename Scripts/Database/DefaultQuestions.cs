using Godot;
using System.Collections.Generic;

public partial class DefaultQuestions : Node
{
    public override void _Ready()
    {
        List<Question> QuestionList = new List<Question>()
        {

            #region BedDifficulty1

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
            },

            new Question
            {
                MiniGame = "Bed",
                Difficulty = 1,
                IsInPlay = true,
                QuestionText = "Coffee",
                Answers = new List<KeyValuePair<string, bool>>
                {
                    new KeyValuePair<string, bool>("Beer", true),
                    new KeyValuePair<string, bool>("Sugar", false)
                }
            },

            new Question
            {
                MiniGame = "Bed",
                Difficulty = 1,
                IsInPlay = true,
                QuestionText = "Savage",
                Answers = new List<KeyValuePair<string, bool>>
                {
                    new KeyValuePair<string, bool>("Hose", true),
                    new KeyValuePair<string, bool>("Garden", false)
                }
            },

            #endregion

            #region BedDifficulty2

            new Question
            {
                MiniGame = "Bed",
                Difficulty = 2,
                IsInPlay = true,
                QuestionText = "Red + Blue",
                Answers = new List<KeyValuePair<string, bool>>
                {
                    new KeyValuePair<string, bool>("Blue + Red", false),
                    new KeyValuePair<string, bool>("Yellow", true)
                }
            },

            new Question
            {
                MiniGame = "Bed",
                Difficulty = 2,
                IsInPlay = true,
                QuestionText = "100 - 100",
                Answers = new List<KeyValuePair<string, bool>>
                {
                    new KeyValuePair<string, bool>("Zero", false),
                    new KeyValuePair<string, bool>("Cellar", true)
                }
            }


            #endregion

        };


        foreach(var Quest in QuestionList)
            DatabaseHandler.QuestionCollection.Insert(Quest);
    }
}