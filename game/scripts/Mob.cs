using System.Threading.Tasks;
using Godot;

namespace therorogame.scripts
{
    public class Mob : KinematicBody2D, Damageable
    {
        public bool Invicible = false;
        [Export()] public int MaxLife = 100;
        [Export] public int HealthGiver = 10;
        [Export()] public int Life = 100;

        public void MakeDamages(int damages)
        {
            Life -= damages;
            if (Life <= 0)
            {
                Death();
            }

            GetDamages();
        }


        public virtual void Death()
        {
            Events events = (Events) GetNode("/root/events");
            QueueFree();
            events.EmitSignal(nameof(Events.GiveHealth), HealthGiver);
        }

        public virtual async void GetDamages()
        {
            Invicible = true;

            GetNode<AnimatedSprite>("AnimatedSprite").Modulate = Colors.OrangeRed;
            await ToSignal(GetTree().CreateTimer(2f), "timeout");
            GetNode<AnimatedSprite>("AnimatedSprite").Modulate = Colors.White;
            Invicible = false;
        }


        public bool CanTakeDamage()
        {
            return !Invicible;
        }
    }
}