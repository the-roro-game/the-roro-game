using Godot;

namespace therorogame.scripts.shop
{
    public class StatsItem : HBoxContainer
    {
        [Signal]
        public delegate void ViewStatItem(BaseStatModifier modifier);

        private BaseStatModifier _currItem = null;

        public override void _Ready()
        {
            GetNode<Button>("ViewButton").Connect("pressed", this, nameof(OnPressedView));
        }

        public void OnPressedView()
        {
            GD.Print("clicked");
            EmitSignal(nameof(ViewStatItem), _currItem);
        }

        public void UpdateItem(BaseStatModifier modifier)
        {
            _currItem = modifier;
            GetNode<TextureRect>("Icon").Texture = modifier.Icon;
            GetNode<Label>("StatName").Text = modifier.StatName;
        }
    }
}