using Godot;
using System;

public class NotificationItem : HBoxContainer
{
    private Texture _icon;

    public Texture Icon
    {
        get => _icon;
        set
        {
            _icon = value;
            GetNode<TextureRect>("icon").Texture = value;
        }
    }
    
    // private Texture _icon;
    //
    // public Texture Icon
    // {
    //     get => _icon;
    //     set
    //     {
    //         _icon = value;
    //         GetNode<TextureRect>("icon").Texture = value;
    //     }
    // }

    public override void _Ready()
    {
    }
}