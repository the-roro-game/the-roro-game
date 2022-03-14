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
            GD.Print("velocity:",player.FacingDirection);
            Bullet inst = Bullet.Instance<Bullet>();
            inst.Position = player.GlobalPosition;
            inst.Velocity = player.FacingDirection - player.GlobalPosition;
            GetTree().CurrentScene.AddChild(inst);
            StateMachine.TransitionTo("PlayerIdle");
        }
    }
}