using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Critter : MonoBehaviour
{
    public int range = 3;
    public bool isMoving;
    float moveTime = 0.2f;

    Tile currentTile;

    public void PlaceCritter(Tile tile)
    {
        transform.position = tile.transform.position;
        currentTile = tile;
        tile.PlaceCritter(this);
    }

    public async void MoveCritter(Tile tile)
    {
        if (isMoving) return;

        isMoving = true;

        currentTile.RemoveCritter();
        tile.PlaceCritter(this);
        currentTile = tile;

        float time = 0;

        Vector3 originPos = transform.position;
        Vector3 targetPos = tile.transform.position;

        while(time < moveTime)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (time / moveTime));
            time += Time.deltaTime;
            await Task.Yield();
        }
        transform.position = targetPos;
        isMoving = false;


    }

    public void SelectCritter()
    {
        GameManager.Instance.SelectedCritter = this;
        GetAvailableTiles();
        PlayerManager.Instance.UpdatPlayerState(PlayerState.CritterSelected);
    }


    public void GetAvailableTiles()
    {
        List<Tile> validTiles = new List<Tile>();
        List<Tile> checkedTiles = new List<Tile>();
        validTiles.Add(currentTile);

        for (int i = 0; i < range; i++)
        {
            List<Tile> cT = new List<Tile>();
            foreach (Tile tile in validTiles)
            {
                if (checkedTiles.Contains(tile)) { continue; }
                foreach (Tile neighbour in tile.neighbours)
                {
                    if (neighbour.walkable && !neighbour.critter && !validTiles.Contains(neighbour))
                    {
                        cT.Add(neighbour);
                    }
                }
                checkedTiles.Add(tile);
            }

            foreach (Tile t in cT)
            {
                validTiles.Add(t);
            }
        }

        foreach(Tile tile in validTiles)
        {
            tile.MakeTileValid();
        }
    }
}
