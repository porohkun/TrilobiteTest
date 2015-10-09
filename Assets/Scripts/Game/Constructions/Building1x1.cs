using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Building1x1 : ConstructionBase
{
    public override void Awake()
    {
        Size = new Point(1, 1);
        Removable = true;
    }
}