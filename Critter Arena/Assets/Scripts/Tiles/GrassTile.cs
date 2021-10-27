using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
    [SerializeField] private Color _baseColor, _alternateColor;

    public override void InitTile(bool alternateColor)
    {
        _spriteRenderer.color = alternateColor ? _alternateColor : _baseColor;
        walkable = true;
    }
}
