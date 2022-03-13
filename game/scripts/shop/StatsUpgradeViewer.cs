using System;
using Godot;

namespace therorogame.scripts.shop
{
    public class StatsUpgradeViewer : UpgradeViewer<BaseStatModifier>
    {
        [Export] public NodePath StatName;
        [Export] public NodePath StatDesc;
        [Export] public NodePath StatIcon;
        [Export] public NodePath StatPrice;
        [Export] public NodePath StatQuantity;
        [Export] public NodePath BuyModifier;

        private BaseStatModifier Upgrade;

        public override void _Ready()
        {
            StatsManager statsManager = (StatsManager) GetTree().Root.GetNode("stats");
            statsManager.Connect(nameof(StatsManager.StatsChanged), this, nameof(OnStatUpdate));
            GetNode<Button>(BuyModifier).Connect("pressed", this, nameof(OnBuyModifier));
        }

        public void OnStatUpdate()
        {
            ViewUpgrade(Upgrade);
        }

        public void OnBuyModifier()
        {

            StatsManager statsManager = (StatsManager) GetTree().Root.GetNode("stats");
            StatHandler<int> coinsStats = statsManager.GetStat<int>("CoinsStat");
            GD.Print("coins ", coinsStats.Value," ",CalculateModifierFinalPrice());


            statsManager.UpdateStat<int>("CoinsStat", coinsStats.Value - CalculateModifierFinalPrice());
            statsManager.AddModifier<int>(Upgrade);
        }

        public int CalculateModifierFinalPrice()
        {
            StatsManager statsManager = (StatsManager) GetTree().Root.GetNode("stats");
            StatHandler<int> statHandler = statsManager.GetStat<int>(Upgrade.StatName);
            int modifiersCount = statHandler.modifiers.Count;

            return (int) Math.Ceiling(Upgrade.Cost * (Math.Pow(Upgrade.CostMultiplicator, modifiersCount)));
        }

        public override void ViewUpgrade(BaseStatModifier upgrade)
        {
            Upgrade = upgrade;
            StatsManager statsManager = (StatsManager) GetTree().Root.GetNode("stats");
            StatHandler<int> statHandler = statsManager.GetStat<int>(upgrade.StatName);
            StatHandler<int> coinsStats = statsManager.GetStat<int>("CoinsStat");
            int modifiersCount = statHandler.modifiers.Count;

            GetNode<Label>(StatName).Text = upgrade.StatName;
            GetNode<Label>(StatDesc).Text = upgrade.StatDesc;
            GetNode<TextureRect>(StatIcon).Texture = upgrade.Icon;

            int finalCost = CalculateModifierFinalPrice();
            GetNode<Label>(StatPrice).Text = $"{finalCost}$";
            Color priceColor = finalCost <= coinsStats.Value ? Colors.Green : Colors.Red;
            GetNode<Label>(StatPrice).SelfModulate = priceColor;
            GetNode<Button>(BuyModifier).Disabled = finalCost > coinsStats.Value;

            GetNode<Label>(StatQuantity).Text = $"x{modifiersCount.ToString()}";
        }
    }
}