using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorTiler_sc : MonoBehaviour
{

    public List<GameObject> tilesTypes;

    public int XSize;
    public int YSize;
    public Vector3 startPos;


    public void Start()
    {
        TILETHAT(XSize, YSize, startPos);
    }


    private void TILETHAT(int x, int y, Vector3 startPos)
    {
        var mesh = tilesTypes[0].GetComponent<MeshRenderer>();
        float tileWidth = mesh.bounds.size.x;        
        Vector3 currPos = startPos;

        for (int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                var curTile = Instantiate(tilesTypes[Random.Range(0, tilesTypes.Count)], currPos, Quaternion.identity);
                curTile.transform.parent = this.transform;
                currPos.x += tileWidth;
            }
            currPos.x = startPos.x;
            currPos.z += tileWidth;
        }
    }



}
