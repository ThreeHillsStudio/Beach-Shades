using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : MonoBehaviour
{
    // Start is called before the first frame update
    public Color sprayColor;
    private ParticleSystemRenderer _sprayParticle;
    private Material _sprayMaterial;

    public static Spray instance;

    public GameObject sprayPosition;

    public bool isSprayActive = true;
    private void Awake()
    {
        instance = this; 
        _sprayMaterial = GetComponent<MeshRenderer>().materials[0];
        _sprayParticle = transform.GetChild(0).GetComponent<ParticleSystemRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
       _sprayParticle.enabled = isSprayActive;
    }

    public void SetColor(Color color)
    {
        sprayColor = color;
        _sprayMaterial.color = color;
        color.a = _sprayParticle.material.color.a;
        _sprayParticle.material.color = color;

    }
    public IEnumerator MoveSpraySprite(float time, GameObject sprite, Vector2 position)
    {
        var transform = sprite.GetComponent<RectTransform>();
        var lastY = transform.position;
        for (var i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(time / 100);
            transform.position = Vector3.Lerp(lastY, position, i / 100);
        }
    }
}
