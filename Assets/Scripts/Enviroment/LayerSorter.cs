﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour
{

    private SpriteRenderer parentRenderer;

 

    private List<Obstacle> obstacles = new List<Obstacle>();

    // Start is called before the first frame update
    void Start()
    {
        parentRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle" || collision.tag == "Minion" || collision.tag == "Enemy")
        {
            Obstacle o = collision.GetComponent<Obstacle>();
            o.FadeOut();
            if (obstacles.Count == 0 || o.MySpriteRenderer.sortingOrder -1 < parentRenderer.sortingOrder)
            {
                parentRenderer.sortingOrder = o.MySpriteRenderer.sortingOrder - 2;
               transform.parent.Find("OutfitSocket").GetComponent<SpriteRenderer>().sortingOrder = o.MySpriteRenderer.sortingOrder -1; //set the outfit in front of the player
            }

            obstacles.Add(o);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle" || collision.tag == "Minion" || collision.tag == "Enemy")
        {
            Obstacle o = collision.GetComponent<Obstacle>();
            o.FadeIn();
            obstacles.Remove(o);

            if (obstacles.Count == 0)
            {
                parentRenderer.sortingOrder = 200;
               transform.parent.Find("OutfitSocket").GetComponent<SpriteRenderer>().sortingOrder = 201;// set the outfit in front of the player
            }
            else
            {
                obstacles.Sort();
                parentRenderer.sortingOrder = obstacles[0].MySpriteRenderer.sortingOrder - 1;
                transform.parent.Find("OutfitSocket").GetComponent<SpriteRenderer>().sortingOrder = obstacles[0].MySpriteRenderer.sortingOrder - 1;
            }
        }
       
    }
}