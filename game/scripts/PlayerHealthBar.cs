using Godot;


namespace therorogame.scripts
{
    public class PlayerHealthBar : Control
    {
        public Texture GreenBar = ResourceLoader.Load<Texture>("res://arts/gui/greenbar.png");
        public Texture YellowBar = ResourceLoader.Load<Texture>("res://arts/gui/yellowbar.png");
        public Texture RedBar = ResourceLoader.Load<Texture>("res://arts/gui/redbar.png");

        public override void _Ready()
        {
            StatsManager statsManager = (StatsManager) GetNode(AutoloadPath.STATS_PATH);
            statsManager.Connect(nameof(StatsManager.StatsChanged), this, nameof(UpdateViewer));
            UpdateViewer();
        }

        public void UpdateViewer()
        {
            StatsManager statsManager = (StatsManager) GetNode(AutoloadPath.STATS_PATH);
            UpdateBar(statsManager);
            UpdateValues(statsManager);
        }


        private void UpdateBar(StatsManager statsManager)
        {
            var HealthBar = GetNode<TextureProgress>("HealthBar");
            Tween tween = new Tween();
            AddChild(tween);
            tween.InterpolateProperty(HealthBar, "value", HealthBar.Value, statsManager.Life, 1f,
                Tween.TransitionType.Back);
            tween.Start();
            HealthBar.MaxValue = statsManager.MaxLife;
            HealthBar.TextureProgress_ = GreenBar;
            if (statsManager.Life < statsManager.MaxLife * 0.7)
            {
                HealthBar.TextureProgress_ = YellowBar;
            }

            if (statsManager.Life < statsManager.MaxLife * 0.35)
            {
                HealthBar.TextureProgress_ = RedBar;
            }

            // RemoveChild(tween);
            // tween.QueueFree();
        }

        private void UpdateValues(StatsManager statsManager)
        {
            var Value = GetNode<Label>("HealthValues");
            Value.Text = $"{statsManager.Life}/{statsManager.MaxLife}";
        }
    }
}