using Godot;

namespace therorogame.data
{
	public class BaseLevel : Resource
	{
		[Export] public string LevelName = "Level";
		[Export] public PackedScene LevelScene;
		public PackedScene SavedScene = null;

		public override string ToString()
		{
			return LevelName;
		}
	}
}
