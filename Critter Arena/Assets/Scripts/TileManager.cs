using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;
    [SerializeField] private int _width, _height;
    [SerializeField] private GameObject _grassTile, _mountainTile, _waterTile;
    [SerializeField] private Transform _cam;

    [SerializeField] public GameObject _testCritter;
    
    private Dictionary<Vector2, Tile> tiles;

    private void Awake()
    {
        Instance = this;

        GameManager.OnGameStateChange += GameManagerOnGameStateChange;
    }

    private void GameManagerOnGameStateChange(GameState state)
    {
        switch (GameManager.Instance.State)
        {
            case GameState.AwaitAction:
                foreach (KeyValuePair<Vector2, Tile> kvPair in tiles)
                {
                    kvPair.Value.ResetTile();
                }
                break;
            case GameState.UnitSelected:
                break;
        }
    }

    void Start()
    {
        GenerateGrid();

        Critter c1 = Instantiate(_testCritter).GetComponent<Critter>();
        Critter c2 = Instantiate(_testCritter).GetComponent<Critter>();
        Critter c3 = Instantiate(_testCritter).GetComponent<Critter>();

        c1.PlaceCritter(GetTile(new Vector2(4, 4)));
        c2.PlaceCritter(GetTile(new Vector2(10, 5)));
        c3.PlaceCritter(GetTile(new Vector2(8, 1)));

        foreach (KeyValuePair<Vector2, Tile> kvPair in tiles)
        {
            kvPair.Value.SetNeighbours(kvPair.Key);
        }
    }

    private void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                int randomInt = Random.Range(0, 10);
                GameObject tile = _grassTile;

                if (randomInt < 2) tile = _mountainTile;
                else if (randomInt < 3) tile = _waterTile;
                else tile = _grassTile;

                Tile newTile = Instantiate(tile, new Vector2(j, i), Quaternion.identity).GetComponent<Tile>();

                newTile.name = $"Tile {j} {i}";

                bool alternateColor = (i + j) % 2 == 0 ? false : true;
                newTile.InitTile(alternateColor);

                tiles.Add(new Vector2(j, i), newTile);
            }
        }

       _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tile GetTile(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile)) {
            return tile;
        }
        else
        {
            return null;
        }
    }
}
