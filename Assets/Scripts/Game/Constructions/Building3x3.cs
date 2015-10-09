using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Building3x3 : ConstructionBase
{
    public override void Awake()
    {
        Size = new Point(3, 3);
        Removable = true;
    }
}
