using Godot;
using LiteDB;

public partial class BedGame : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var LeftLabel = GetNode<Label>("./LeftLabel");
		var RightLabel = GetNode<Label>("./RightLabel");

		var FirstQuestion = DatabaseHandler.QuestionCollection.FindOne(x => x.MiniGame == "Bed");

		var FirstAnswer = FirstQuestion.Answers[0];
		var SecondAnswer = FirstQuestion.Answers[1];
		LeftLabel.Text = FirstAnswer.Key;
		RightLabel.Text = SecondAnswer.Key;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	}
}
