using Godot;
using Godot.Collections;

namespace therorogame.scripts.state.player
{
    public class PlayerDistantAttack : State
    {
        [Export] public PackedScene Bullet;

        public override void EnterState(Dictionary<string, string> _datas = null)
        {
        }
    }
}