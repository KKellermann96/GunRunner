using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMult;
    private Transform camereaTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitsSizeX;
    private void Start()
    {
        camereaTransform = Camera.main.transform;
        lastCameraPosition = camereaTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitsSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void FixedUpdate()
    {
        Vector3 deltaMovement = camereaTransform.position - lastCameraPosition;
       
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMult.x, deltaMovement.y * parallaxEffectMult.y);
        lastCameraPosition = camereaTransform.position;

        if(Mathf.Abs(camereaTransform.position.x - transform.position.x) >= textureUnitsSizeX)
        {
            float offsetPositionX = (camereaTransform.position.x - transform.position.x) % textureUnitsSizeX;
            transform.position = new Vector3(camereaTransform.position.x + offsetPositionX, transform.position.y);
        }
    }
    
}
