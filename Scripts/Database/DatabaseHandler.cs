using Godot;
using LiteDB;
using System.IO;

public partial class DatabaseHandler : Node
{
	public static LiteDatabase GameDatabase;
	public static ILiteCollection<Question> QuestionCollection;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var current_directory = System.IO.Directory.GetCurrentDirectory();
		var database_path = System.IO.Path.Combine(current_directory, "game_database.db");

		GD.Print("Initializing database...");

		var connection_string = $"Filename = {database_path}; Password = xyz123; Connection Type = Shared;";
		GameDatabase = new LiteDatabase(connection_string);

		QuestionCollection = GameDatabase.GetCollection<Question>("questions");
        
        
	}	
	
}
