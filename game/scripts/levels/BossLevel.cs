using Godot;

namespace therorogame.scripts.levels
{
    public class BossLevel : Level
    {
        [Export()] public PackedScene BossScene;

        public override async void _Ready()
        {
            Vector2 start = GetNode<Position2D>("StartBoss").Position;
            Vector2 end = GetNode<Position2D>("EndBoss").Position;

            Boss inst = BossScene.Instance<Boss>();
            Tween tween = new Tween();
            AddChild(inst);
            AddChild(tween);
            tween.InterpolateProperty(inst, "position", start, end, 1.5f, Tween.TransitionType.Back);
            tween.Start();
            await ToSignal(tween, "tween_all_completed");

            SpawnPlayer();
            inst.StartShooting();
        }
    }
}