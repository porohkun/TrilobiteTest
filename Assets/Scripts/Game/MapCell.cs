using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MapCell
{
    public Point Position { get; private set; }
    public string Material { get; private set; }
    public ConstructionBase Construction { get; private set; }

    public bool CanBuild { get { return Material != "water" && Construction == null; } }

    private GameMap _map;

    public MapCell(int x, int y)
    {
        Position = new Point(x, y);
    }

    public void Init(GameMap map, string material)
    {
        _map = map;
        Material = material;
    }

    public MapCell GetNeighbour(int x, int y)
    {
        return _map[Position + new Point(x, y)];
    }

    public void PlaceConstruction(ConstructionBase constr, bool root = true)
    {
        Construction = constr;
        if (root)
        {
            constr.SetCell(this);
            for (int x = 0; x < constr.Size.X; x++)
                for (int y = 0; y < constr.Size.Y; y++)
                {
                    if (x != 0 || y != 0)
                        GetNeighbour(x, y).PlaceConstruction(constr, false);
                }
        }
    }

    public void RemoveConstruction()
    {
        if (Construction != null)
        {
            var size = Construction.Size;
            var constr = Construction;
            for (int x = 0; x < constr.Size.X; x++)
                for (int y = 0; y < constr.Size.Y; y++)
                {
                    GetNeighbour(x, y).Construction = null;
                }
            UnityEngine.GameObject.Destroy(constr.gameObject);
        }
    }

}
