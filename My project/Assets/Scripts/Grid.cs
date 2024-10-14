using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int areaResolution = 22;
    public Material groundMaterial;
    public Material snakeMaterial;
    public Material headMaterial;
    public Material fruitMaterial;
    private Renderer[] gameBlocks;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        gameBlocks = new Renderer[areaResolution * areaResolution];
        for (int x = 0; x < areaResolution; x++)
        {
            for (int y = 0; y < areaResolution; y++)
            {
                GameObject quadPrimitive = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quadPrimitive.transform.position = new Vector3(x, 0, y);
                Destroy(quadPrimitive.GetComponent<Collider>());
                quadPrimitive.transform.localEulerAngles = new Vector3(90, 0, 0);
                quadPrimitive.transform.SetParent(transform);
                gameBlocks[(x * areaResolution) + y] = quadPrimitive.GetComponent<Renderer>();
            }
        }
    }
}
