using Godot;
using System;


public class BaseStatModifier : Resource
{
    [Export] public string StatName = "";
    [Export] public string StatDesc = "";
    [Export] public Texture Icon;
    [Export] public string ModifierType = "+";
    [Export] public int ModifierAdder = 5;
    [Export] public int Cost = 1;
    [Export] public int CostMultiplicator = 2;
}