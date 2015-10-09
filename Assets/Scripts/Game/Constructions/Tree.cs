using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Tree : ConstructionBase
{
    public MeshRenderer Renderer2;
    public Material LeavesMaterial;
    public override void Awake()
    {
        Size = new Point(3, 3);
        Removable = false;
    }

    public override void MakeVisible(bool enable)
    {
        base.MakeVisible(enable);

        Renderer2.material = enable ? LeavesMaterial : TransparentMaterial;
        Renderer2.shadowCastingMode = enable ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
    }
}
