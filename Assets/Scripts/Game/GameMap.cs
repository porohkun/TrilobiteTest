using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class GameMap : MonoBehaviour
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    private MapCell[,] _cells;
    public MeshFilter Mesh;
    public Cursor Cursor;
    private Vector3 _normalTop = new Vector3(0, 1, 0);

    public MapCell this[Point position]
    {
        get
        {
            return this[position.X, position.Y];
        }
    }
    public MapCell this[int x, int y]
    {
        get
        {
            return CorrectPosition(x, y) ? _cells[x, y] : null;
        }
    }

    void Start()
    {
        var input = GetComponent<InputController>();
    }

    public void Init(int width, int height)
    {
        Width = width;
        Height = height;
        _cells = new MapCell[width, height];
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                _cells[x, y] = new MapCell(x, y);
        FillGround();
        GenerateSurface();
    }

    public bool CorrectPosition(Point position)
    {
        return CorrectPosition(position.X, position.Y);
    }
    public bool CorrectPosition(int x, int y)
    {
        return x >= 0 && y >= 0 && x < Width && y < Height;
    }

    void FillGround()
    {
        var treePrefab = Resources.Load<ConstructionBase>("Prefabs/Tree");
        float xo = Random.value * 100f;
        float yo = Random.value * 100f;
        int pathless = 0;
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                float height = Mathf.PerlinNoise((float)x / Width * 5 + xo, (float)y / Height * 5 + yo);
                string material = height > 0.5f ? "grass" : (height > 0.2f ? "dirt" : "water");
                if (material == "water") pathless++;
                this[x, y].Init(this, material);
            }
        int error = 0;
        while (pathless < 1000 && error<20)
        {
            int tx = Random.Range(0, Width - 3);
            int ty = Random.Range(0, Height - 3);
            Point pos = new Point(tx, ty);
            bool canBuild = true;
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    var cell = this[pos + new Point(x, y)];
                    canBuild = canBuild && cell.CanBuild;
                }
            if (canBuild)
            {
                var tree = Instantiate<ConstructionBase>(treePrefab);
                PlaceBuilding(tree, pos);
                pathless += 9;
                error = 0;
            }
            else
                error++;
        }
        Debug.Log(pathless);
    }

    void GenerateSurface()
    {
        List<PreMesh> preMeshes = new List<PreMesh>();

        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                preMeshes.Add(CreateTopPanel(x, y, Global.GetTexture(this[x, y].Material)));
        Mesh.mesh = PreMesh.ToMesh(preMeshes);
    }

    PreMesh CreateTopPanel(int x, int y, Vector2 tex)
    {
        return new PreMesh
        {
            Vertices = new[]
            {
                new Vector3(x, 0f, y),
                new Vector3(x, 0f, y + 1),
                new Vector3(x + 1, 0f, y),
                new Vector3(x + 1, 0f, y + 1)
            },
            Normals = new[] 
            { 
                _normalTop, _normalTop, _normalTop, _normalTop
            },
            Uv = new[] 
            {
                new Vector2(tex.x, tex.y),
                new Vector2(tex.x, tex.y + Global.PartSize),
                new Vector2(tex.x + Global.PartSize, tex.y),
                new Vector2(tex.x + Global.PartSize, tex.y + Global.PartSize)
            },
            Triangles = new[]
            {
                0, 1, 2,
                1, 3, 2
            }
        };
    }

    public void PlaceBuilding(ConstructionBase building, Point position)
    {
        this[position].PlaceConstruction(building);
        building.transform.SetParent(transform);
        building.transform.localPosition = position;
    }
}
