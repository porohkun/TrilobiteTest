using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class ConstructionBase:MonoBehaviour
{
    public MapCell Cell { get; private set; }
    public MeshRenderer Renderer;
    public Material FinalMaterial;
    public Material TransparentMaterial;
    public Point Size { get; protected set; }
    public bool Removable { get; protected set; }

    public virtual void Awake()
    {
        Size = new Point(0, 0);
        Removable = true;
    }

    public virtual void SetCell(MapCell cell)
    {
        Cell = cell;
        MakeVisible(true);
    }

    public virtual void MakeVisible(bool enable)
    {
        Renderer.material = enable ? FinalMaterial : TransparentMaterial;
        Renderer.shadowCastingMode = enable ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    public virtual void Remove()
    {
        Cell.RemoveConstruction();
    }
}

