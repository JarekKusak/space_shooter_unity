using UnityEngine;
using System.Collections.Generic;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject tilePrefab; // Prefab dlaždice
    public int numTilesX; // Počet dlaždic v horizontálním směru
    public int numTilesY; // Počet dlaždic ve vertikálním směru

    private Vector2 tileSize; // Velikost dlaždice
    private Transform playerTransform; // Transform hráče
    private List<GameObject> tiles; // Seznam všech dlaždic

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Najděte hráče pomocí tagu
        tileSize = tilePrefab.GetComponent<SpriteRenderer>().bounds.size; // Získejte rozměr dlaždice
        CreateTiles();
    }

    // Vytvoří dlaždice, které vyplní celou obrazovku a trochu navíc
    private void CreateTiles()
    {
        tiles = new List<GameObject>();
        for (int i = -numTilesX; i < numTilesX; i++)
        {
            for (int j = -numTilesY; j < numTilesY; j++)
            {
                Vector2 positionOffset = new Vector2(i * tileSize.x, j * tileSize.y);
                GameObject tile = Instantiate(tilePrefab, (Vector2)playerTransform.position + positionOffset, Quaternion.identity);
                tile.transform.parent = transform; // Nastavíme rodiče dlaždic pro lepší organizaci v hierarchii
                tiles.Add(tile);
            }
        }
    }

    private void Update()
    {
        
        Camera cam = Camera.main;
        float camVerticalExtend = cam.orthographicSize * 2f;
        float camHorizontalExtend = camVerticalExtend * cam.aspect;

        // Získáme hranice kamery
        Vector2 camMin = (Vector2)cam.transform.position - new Vector2(camHorizontalExtend, camVerticalExtend) / 2f;
        Vector2 camMax = (Vector2)cam.transform.position + new Vector2(camHorizontalExtend, camVerticalExtend) / 2f;

        foreach (var tile in tiles)
        {
            RepositionTile(tile, camMin, camMax);
        }
    }

    private void RepositionTile(GameObject tile, Vector2 camMin, Vector2 camMax)
    {
        
        Vector2 tilePos = tile.transform.position;
    
        // Zkontrolujeme, jestli je dlaždice vlevo nebo vpravo od kamery
        if (tilePos.x < camMin.x)
        {
            tilePos.x += tileSize.x * numTilesX * 2; // Posune dlaždici doprava
        }
        else if (tilePos.x > camMax.x)
        {
            tilePos.x -= tileSize.x * numTilesX * 2; // Posune dlaždici doleva
        }

        // Zkontrolujeme, jestli je dlaždice nad nebo pod kamerou
        if (tilePos.y < camMin.y)
        {
            tilePos.y += tileSize.y * numTilesY * 2; // Posune dlaždici nahoru
        }
        else if (tilePos.y > camMax.y)
        {
            tilePos.y -= tileSize.y * numTilesY * 2; // Posune dlaždici dolů
        }

        tile.transform.position = tilePos;
    }
}
