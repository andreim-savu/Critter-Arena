﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainTile : Tile
{
    public override void InitTile(bool alternateColor)
    {
        base.InitTile(alternateColor);
        walkable = false;
    }
}
