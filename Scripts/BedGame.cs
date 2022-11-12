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
	Control PlayerStartPosition;

	Question Question;

	Random RandomQuestionNumber = new Random();
	Random RandomLoopNumber = new Random();

	//bool MovementTweenActive = false;
	public enum MiniGameState {PLAYING, SELECTING_ANSWER, ANSWER_SELECTED, CORRECT_ANSWER_SELECTED, INCORRECT_ANSWER_SELECTED};
	private MiniGameState CurrentState = MiniGameState.PLAYING;


	public int Difficulty = 1;
	public int ConsecutiveCorrect = 0;





	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LeftLabel = GetNode<Label>("./LeftLabel");
		RightLabel = GetNode<Label>("./RightLabel");
		QuestionLabel = GetNode<Label>("./QuestionLabel");

		// Player node is NOT a child of the CanvasLayer, so get the parent first (essenitally, end up with the "sibling" node)
		Player = GetNode<Node2D>("../Player");
		PlayerStartPosition = GetNode<Control>("./StartPosition2D");

		LoadQuestion();
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("ui_left"))
		{
			if (CurrentState == MiniGameState.PLAYING)
			{
				MoveToAnswer(LeftLabel);
				CheckAnswer(LeftLabel.Text);
			}
		}
		else if (Input.IsActionJustPressed("ui_right"))
		{
			if (CurrentState == MiniGameState.PLAYING)
			{
				MoveToAnswer(RightLabel);
				CheckAnswer(RightLabel.Text);
			}
		}

	}

	



	private void LoadQuestion()
	{
		if (ConsecutiveCorrect >= 10 && Difficulty == 1)
		{
			Difficulty += 1;
			GD.Print($"Difficulty is now level {Difficulty}!");
		}

		Player.GetNode<Sprite>("Sprite").Position = PlayerStartPosition.RectPosition;

		Question = DatabaseHandler.QuestionCollection.Find(x => x.MiniGame == "Bed" && x.Difficulty == Difficulty).OrderBy(x => RandomQuestionNumber.Next()).First();

		// Set the question text
		QuestionLabel.Text = Question.QuestionText;

		var AnswerLabels = GetTree().GetNodesInGroup("AnswerLabels");

		// Randomize the order of the Label objects
		AnswerLabels.Shuffle();

		// Set the answers
		foreach(int i in Enumerable.Range(0, Question.Answers.Count).OrderBy(x => RandomLoopNumber.Next()))
		{
			// This will be random because of the shuffling of the Label array above
			(AnswerLabels[i] as Label).Text = Question.Answers[i].Key;
		}
	}


	
	private async void MoveToAnswer(Label Target)
	{
		// RectPosition returns the top-left corner, we desire the center
		var TargetPosition = Target.RectPosition + Target.RectScale * Target.RectSize / 2.0f;

		var Twn = CreateTween();
		CurrentState = MiniGameState.SELECTING_ANSWER;

		Twn.TweenProperty(Player.GetNode("Sprite"), "position", TargetPosition, .5f);
		await ToSignal(Twn, "finished");

		CurrentState = MiniGameState.ANSWER_SELECTED;
		
	}



	private async void CheckAnswer(string AnswerText)
	{
		
		var Answer = Question.Answers.Where(x => x.Key == AnswerText).FirstOrDefault();

		if (Answer.Value == true)
		{
			CurrentState = MiniGameState.CORRECT_ANSWER_SELECTED;
			ConsecutiveCorrect += 1;
			GD.Print("Good job, WRONG answer!");

			var DelayTimer = GetTree().CreateTimer(2.5f);
			await ToSignal(DelayTimer, "timeout");

			CurrentState = MiniGameState.PLAYING;
			LoadQuestion();
		}
		else if (Answer.Value == false)
		{
			CurrentState = MiniGameState.INCORRECT_ANSWER_SELECTED;
			ConsecutiveCorrect = 0;
			GD.Print("Incorrect, RIGHT answer!");

			var DelayTimer = GetTree().CreateTimer(2.5f);
			await ToSignal(DelayTimer, "timeout");

			CurrentState = MiniGameState.PLAYING;
			LoadQuestion();
		}
		

	}
}
