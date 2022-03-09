using Godot;
using System;
using therorogame.data;
using therorogame.scripts;
using Path = System.IO.Path;

public class PossibleDirections
{
    public bool Up = false;
    public bool Right = false;
    public bool Down = false;
    public bool Left = false;

    public override string ToString()
    {
        return $"{(Up ? '^' : 'x')}{(Right ? '>' : 'x')}{(Down ? 'V' : 'x')}{(Left ? '<' : 'x')}";
    }
}

public class LevelsManager : Node
{
    private BaseLevel _currLevel;

    public BaseLevel CurrLevel
    {
        get => _currLevel;
        set
        {
            _currLevel = value;
            GetTree().ChangeSceneTo(_currLevel.LevelScene);
        }
    }

    private int _X;

    public int X
    {
        get => _X;
        set
        {
            _X = value;
            CurrLevel = Map[Y, X];
        }
    }

    private int _Y;

    public int Y
    {
        get => _Y;
        set
        {
            _Y = value;
            CurrLevel = Map[Y, X];
        }
    }

    private const int WIDTH = 4;
    private const int HEIGHT = 2;
    public BaseLevel[,] Map = new BaseLevel[HEIGHT, WIDTH];

    private delegate string CellRenderer(int x, int y);

    public string RootLevelPath = "res://data/levels";

    public override void _Ready()
    {
        Events events = (Events) GetNode("/root/events");
        UpdateLevelMap();
        ShowMap((x, y) => $"{Map[y, x].ToString()}\t");
        ShowMap((x, y) =>
        {
            PossibleDirections directions = GetPossibleDirections(x, y);
            return $"{directions.ToString()}\t";
        });
        events.Connect(nameof(Events.DirectionChange), this, nameof(OnDirectionChange));
    }

    private void ShowMap(CellRenderer Renderer)
    {
        String matrix = "";
        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                if (Map[i, j] != null)
                {
                    matrix += Renderer(j, i);
                }
                else
                {
                    matrix += "null\t";
                }
            }

            matrix += "\n";
        }

        GD.Print(matrix);
    }


    public void UpdateLevelMap()
    {
        var dir = new Directory();
        if (dir.Open(RootLevelPath) == Error.Ok)
        {
            dir.ListDirBegin(true, true);
            var filename = dir.GetNext();
            while (filename != "")
            {
                GD.Print(filename);
                int RoomNb = Path.GetFileNameWithoutExtension(filename).ToInt();
                if (IsValidRoom(RoomNb))
                {
                    int x = RoomNb % WIDTH;
                    int y = RoomNb / WIDTH;
                    BaseLevel level = ResourceLoader.Load<BaseLevel>(RootLevelPath + "/" + filename);
                    Map[y, x] = level;
                }

                filename = dir.GetNext();
            }

            dir.ListDirEnd();
        }
        else
        {
            GD.PrintErr("can't load map");
        }
    }

    public PossibleDirections GetPossibleDirections(int x, int y)
    {
        var possible = new PossibleDirections
        {
            Up = y > 0 && y < HEIGHT && Map[y - 1, x] != null,
            Down = y >= 0 && (y < HEIGHT - 1) && Map[y + 1, x] != null,
            Left = x > 0 && x < WIDTH && Map[y, x - 1] != null,
            Right = x >= 0 && (x < WIDTH - 1) && Map[y, x + 1] != null
        };

        return possible;
    }

    private int MaxRoom()
    {
        return (WIDTH * HEIGHT) - 1;
    }

    private bool IsValidRoom(int room)
    {
        return room >= 0 && room <= MaxRoom();
    }


    public void LoadLevel(int x, int y)
    {
        Events events = (Events) GetNode("/root/events");
        Y = y;
        X = x;
    }

    public void OnDirectionChange(int x, int y)
    {
        LoadLevel(X + x, Y + y);
    }
}