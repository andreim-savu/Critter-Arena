using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _moveHighlight;

    public Critter critter;
    public List<Tile> neighbours = new List<Tile>();

    public bool walkable;
    bool valid;

    public virtual void InitTile(bool alternateColor) { }

    public virtual void SetNeighbours(Vector2 pos)
    {
        Tile neighbour = TileManager.Instance.GetTile(pos + new Vector2(1, 0));
        if (neighbour) { neighbours.Add(neighbour); }
        neighbour = TileManager.Instance.GetTile(pos + new Vector2(0, 1));
        if (neighbour) { neighbours.Add(neighbour); }
        neighbour = TileManager.Instance.GetTile(pos - new Vector2(1, 0));
        if (neighbour) { neighbours.Add(neighbour); }
        neighbour = TileManager.Instance.GetTile(pos - new Vector2(0, 1));
        if (neighbour) { neighbours.Add(neighbour); }
    }

    public void PlaceCritter(Critter c)
    {
        critter = c;
    }

    public void RemoveCritter()
    {
        critter = null;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        Select();
    }

    private void Select()
    {
        switch(GameManager.Instance.State)
        {
            case GameState.AwaitAction:
                if (!critter) { return; }
                critter.SelectCritter();
                break;
            case GameState.UnitSelected:
                if (critter)
                {
                    GameManager.Instance.UpdateGameState(GameState.AwaitAction);
                    critter.SelectCritter();
                }

                else
                {
                    if (valid)
                    {
                        GameManager.Instance.SelectedCritter.MoveCritter(this);
                    }
                    GameManager.Instance.UpdateGameState(GameState.AwaitAction);
                }
                break;
        }
    }

    public void MakeTileValid()
    {
        valid = true;
        _moveHighlight.SetActive(true);
    }

    public void ResetTile()
    {
        valid = false;
        _moveHighlight.SetActive(false);
    }
}
