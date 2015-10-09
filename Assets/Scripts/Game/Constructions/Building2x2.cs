using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Building2x2 : ConstructionBase
{
    public override void Awake()
    {
        Size = new Point(2, 2);
        Removable = true;
    }
}
