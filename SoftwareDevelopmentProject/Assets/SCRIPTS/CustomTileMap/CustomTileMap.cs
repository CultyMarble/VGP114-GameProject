using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTileMap
    : MonoBehaviour
{
    [SerializeField] private Transform pf_tile;

    [Header("Tile Settings:")]
    [SerializeField] private float offsetFromOriginal;
    [SerializeField] private int tileScale;

    [Header("Map Dimension:")]
    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;

    private Vector2 tilePosition;

    //======================================================================
    private void Start()
    {
        for (int widthIndex = 0; widthIndex < mapWidth; widthIndex++)
        {
            for (int heightIndex = 0; heightIndex < mapHeight; heightIndex++)
            {
                float xPos = (float)(widthIndex * tileScale) + offsetFromOriginal;
                float yPos = (float)(heightIndex * tileScale) + offsetFromOriginal;

                tilePosition = new Vector2(xPos, yPos);

                Transform tileTransform = Instantiate(pf_tile, tilePosition, Quaternion.identity);
                tileTransform.SetParent(gameObject.transform);
            }
        }
    }
}