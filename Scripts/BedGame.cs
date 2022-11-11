using System;
using System.Linq;
using System.Collections.Generic;
using Godot;
using LiteDB;

public partial class BedGame : CanvasLayer
{
	Label LeftLabel;
	Label RightLabel;
	Label QuestionLabel;
	Node2D Player;
	Question Question;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LeftLabel = GetNode<Label>("./LeftLabel");
		RightLabel = GetNode<Label>("./RightLabel");

		// Player node is NOT a child of the CanvasLayer, so get the parent first (essenitally, end up with the "sibling" node)
		Player = GetNode<Node2D>("../Player");

		Question = DatabaseHandler.QuestionCollection.FindOne(x => x.MiniGame == "Bed");

		// Set the question text
		QuestionLabel = GetNode<Label>("./QuestionLabel");
		QuestionLabel.Text = Question.QuestionText;

		// Set the answers
		// Randomize loop
		Random r = new Random();
		foreach (int i in Enumerable.Range(0, Question.Answers.Count).OrderBy(x => r.Next()))
		{
			if (LeftLabel.Text == "#")
				LeftLabel.Text = Question.Answers[i].Key;
			else if (RightLabel.Text == "#")
				RightLabel.Text = Question.Answers[i].Key;
		}

	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var AnswerText = "";

		if (Input.IsActionJustPressed("ui_left"))
		{
			MoveToAnswer(LeftLabel);
			CheckAnswer(LeftLabel.Text);
		}
		else if (Input.IsActionJustPressed("ui_right"))
		{
			MoveToAnswer(RightLabel);
			CheckAnswer(RightLabel.Text);
		}

	}



	private void MoveToAnswer(Label Target)
	{
		// RectPosition returns the top-left corner, we desire the center
		var TargetPosition = Target.RectPosition + Target.RectScale * Target.RectSize / 2.0f;

		var Tween = CreateTween();
		Tween.TweenProperty(Player.GetNode("Sprite"), "position", TargetPosition, .5f);
	}

	private void CheckAnswer(string AnswerText)
	{
		var Answer = Question.Answers.Where(x => x.Key == AnswerText).FirstOrDefault();

		if (Answer.Value == true)
			GD.Print("Good job, WRONG answer!");
		else if (Answer.Value == false)
			GD.Print("Incorrect, RIGHT answer!");

	}
}
