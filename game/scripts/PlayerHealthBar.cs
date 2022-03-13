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
            if (statsManager != null)
            {
                UpdateBar(statsManager);
                UpdateValues(statsManager);
            }
        }


        private void UpdateBar(StatsManager statsManager)
        {
            var HealthBar = GetNode<TextureProgress>("HealthBar");
            int Life = statsManager.GetStat<int>("LifeStat").Value;
            int MaxLife = statsManager.GetStat<int>("MaxLifeStat").Value;


            Tween tween = new Tween();
            AddChild(tween);
            tween.InterpolateProperty(HealthBar, "value", HealthBar.Value, Life, 1f,
                Tween.TransitionType.Back);
            tween.Start();
            HealthBar.MaxValue = MaxLife;
            HealthBar.TextureProgress_ = GreenBar;
            if (Life < MaxLife * 0.7)
            {
                HealthBar.TextureProgress_ = YellowBar;
            }

            if (Life < MaxLife * 0.35)
            {
                HealthBar.TextureProgress_ = RedBar;
            }
        }

        private void UpdateValues(StatsManager statsManager)
        {
            int Life = statsManager.GetStat<int>("LifeStat").Value;
            int MaxLife = statsManager.GetStat<int>("MaxLifeStat").Value;
            var Value = GetNode<Label>("HealthValues");

            Value.Text = $"{Life}/{MaxLife}";
        }
    }
}