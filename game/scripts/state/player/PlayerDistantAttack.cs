using Godot;
using Godot.Collections;

namespace therorogame.scripts.state.player
{
    public class PlayerDistantAttack : State
    {
        [Export] public PackedScene Bullet;

        public override void EnterState(Dictionary<string, string> _datas = null)
        {
            Player player = GetOwner<Player>();
            GD.Print("velocity:", player.FacingDirection);
            Node2D inst = Bullet.Instance<Node2D>();
            GetTree().CurrentScene.AddChild(inst);

            inst.Position = player.Position;
            inst.Rotation = player.FacingDirection.Angle();
            StateMachine.TransitionTo("PlayerIdle");
        }
    }
}