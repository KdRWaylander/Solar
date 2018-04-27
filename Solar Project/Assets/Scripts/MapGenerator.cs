using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Terrain tile")]
    [SerializeField] GameObject m_groundTile;
    [SerializeField] float m_tileScale;

    [Header("Terrain parameters")]
    [SerializeField] int m_terrainWidth;
    [SerializeField] float m_terrainResolution;
    [SerializeField] float m_terrainHeight;

    void Start ()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        Vector3 pos = new Vector3(m_terrainWidth / 2f, m_terrainHeight / 4f, m_terrainWidth / 2f);
        transform.position = pos;

        float offsetX = Random.Range(0f, 1000f);
        float offsetY = Random.Range(0f, 1000f);
        InstantiateTerrain(offsetX, offsetY);

        Vector3 centerPos = new Vector3(0, 0, 0);
        transform.position = centerPos;

    }

    void CleanUpTerrain()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void RegenerateTerrain()
    {
        CleanUpTerrain();
        GenerateTerrain();
    }

    void InstantiateTerrain(float _offsetX, float _offsetY)
    {
        for (int i = 0; i < m_terrainWidth; i++)
        {
            for (int j = 0; j < m_terrainWidth; j++)
            {
                GameObject go = Instantiate(m_groundTile, CalculatePosition(i, j, _offsetX, _offsetY), Quaternion.identity, transform);
                Vector3 scale = new Vector3(go.transform.localScale.x * m_tileScale, 1, go.transform.localScale.x * m_tileScale);
                go.transform.localScale = scale;
            }
        }
    }

    Vector3 CalculatePosition(int _x, int _z, float _oX, float _oY)
    {
        float perlinX = (float)_x / m_terrainWidth * m_terrainResolution + _oX; 
        float perlinY = (float)_z / m_terrainWidth * m_terrainResolution + _oY;

        float sample = Mathf.FloorToInt(m_terrainHeight * Mathf.PerlinNoise(perlinX, perlinY));

        return new Vector3(m_tileScale * _x, sample, m_tileScale * _z);
    }
}