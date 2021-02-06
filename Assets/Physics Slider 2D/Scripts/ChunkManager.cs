using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to the object "chunk manager".
public class ChunkManager : MonoBehaviour
{
    public GameObject[] chunkprephabs;
    public GameObject currentTile;
    public Transform scenecamera;
    public float oldcameraX;
 
    // Note: the words "chunk" and "tile" are interchangeable .

    void Start()
    {
        //                  ↓ this is the number of the initial spawned chunks increase it if you see empty space to the right 
        for (int i = 0; i < 3; i++)
        {
            SpawnHill();

        }
        oldcameraX = scenecamera.position.x;
    }

   
    void Update()
    {



   
        
       
        Debug.Log(oldcameraX);
        //                                                   ↓ this number the the rate of spawning of chunks its spawns every 200 unit traveled decrease it if you see chunks despawning under the player
        if (scenecamera.transform.position.x >= oldcameraX+ 200 )
        {
            for (int i = 0; i < 1; i++)
            {
                SpawnHill();
              
            }

        }

        //                         ↓ this is the number of the loaded chunks increase it if you have smaller chunks
        if (transform.childCount > 7)
        {
            DestroyHill();
        }
        
    }
    void SpawnHill()
    {
        // chunks are spawned as children of the object chunk manager.
        currentTile = (GameObject) Instantiate(chunkprephabs[Random.Range(0,chunkprephabs.Length)], currentTile.transform.GetChild(0).transform.position,Quaternion.identity,transform);
        oldcameraX = scenecamera.position.x;
    }
   void DestroyHill()
   {
        Destroy(transform.GetChild(0).gameObject);
       
    }
    
   
}
