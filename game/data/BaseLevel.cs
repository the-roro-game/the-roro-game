using Godot;

namespace therorogame.data
{
	public class BaseLevel : Resource
	{
		[Export] public string LevelName = "Level";
		[Export] public PackedScene LevelScene;
<<<<<<< HEAD
		public PackedScene SavedScene = null;
=======
>>>>>>> matheo

		public override string ToString()
		{
			return LevelName;
		}
	}
}
