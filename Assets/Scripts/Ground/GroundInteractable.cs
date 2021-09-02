using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundInteractable : MonoBehaviour
{
    public RuleTile rt;
    public TilemapCollider2D tc;
    public Tilemap tilemap;
        
        void Start()
    {
        tc = GetComponent<TilemapCollider2D>();
        tilemap = GetComponent<Tilemap>();
    }
    public static Vector3Int RoundToInt(Vector3 v)
    {
        return new Vector3Int(
            Mathf.RoundToInt(v.x),
            Mathf.RoundToInt(v.y),
            Mathf.RoundToInt(v.z)
        );
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        Debug.Log(collision.bounds + " || "+         collision.name);

        Debug.Log(tilemap.GetTile(RoundToInt(collision.bounds.center)));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Animier mich loooos! " + rt.name);
        foreach (RuleTile.TilingRule rule in rt.m_TilingRules)
        {
            rule.m_MaxAnimationSpeed = 1;
            rule.m_MinAnimationSpeed = 1;
        }    

    }


}
