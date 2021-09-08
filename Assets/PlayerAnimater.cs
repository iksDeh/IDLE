using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DragonBones;
public class PlayerAnimater : MonoBehaviour
{
    UnityArmatureComponent player;
    bool cheack = false;

    public Sprite test;
    public MeshFilter meshh;
    public Shader shader;
    public MeshRenderer mr;
    Material mat;
    private void Start()
    {
        player = GetComponent<UnityArmatureComponent>();
        mat = new Material(shader);
        //ma
        SpriteToMesh(test);
    }

    private void Update()
    {

        if (!cheack)
            player.animation.Play("Walk");
        cheack = true;
    }

    Mesh SpriteToMesh(Sprite sprite)
    {

        Mesh mesh = new Mesh();
        mesh.vertices = Array.ConvertAll(sprite.vertices, i => (Vector3)i);
        mesh.uv = sprite.uv;
        mesh.triangles = Array.ConvertAll(sprite.triangles, i => (int)i);
        meshh.mesh = mesh;

        mat.CopyPropertiesFromMaterial(mr.material);
        mat.mainTexture = sprite.texture;
        // mat.
        mr.material = mat;
        return mesh;
    }

}
