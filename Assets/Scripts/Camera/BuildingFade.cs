using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingFade : MonoBehaviour
{
    private Tilemap myTilemap;
    public GameObject wallTilemap;
    public Tilemap myWallTilemap;

    // Start is called before the first frame update
    void Start()
    {
        myTilemap = GetComponent<Tilemap>();
        myWallTilemap = wallTilemap.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Triggers fade of building TileMaps when player moves under building
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("FadeOut");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("FadeIn");
        }
    }

    private IEnumerator FadeOut()
    {
        for (float f = 1f; f >= 0.5f; f -= 0.05f)
        {
            Color c = myTilemap.color;
            c.a = f;
            myTilemap.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        for (float g = 1f; g >= 0f; g -= 0.05f)
        {
            Color d = myWallTilemap.color;
            d.a = g;
            myWallTilemap.color = d;
            yield return new WaitForSeconds(0f);
        }
    }

    private IEnumerator FadeIn()
    {
        for (float f = 0.5f; f <= 1f; f += 0.05f)
        {
            Color c = myTilemap.color;
            c.a = f;
            myTilemap.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        for(float g = 0f; g <= 1f; g += 0.05f)
        {
            Color d = myWallTilemap.color;
            d.a = g;
            myWallTilemap.color = d;
            yield return new WaitForSeconds(0f);
        }
    }
}
