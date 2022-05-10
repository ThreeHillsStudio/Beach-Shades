using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EasyGameStudio.paint;

public class StickerPaint : MonoBehaviour
{
    public GameObject decalPrefab;
    [HideInInspector]
    public GameObject currentSticker;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && SceneController.instance.currentScene == 3)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "paint")
                {
                    if(/*currentSticker == null &&*/ StickerChooser.instance.currentSticker)
                    {
                        var croppedTexture = TextureFromSprite(StickerChooser.instance.currentSticker);
                        
                        //Quaternion.Euler(new Vector3(-45, 0, 0))
                        currentSticker = Instantiate(decalPrefab, hit.point, Quaternion.FromToRotation(Vector3.down, hit.normal));
                        currentSticker.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", croppedTexture);
                        currentSticker.transform.parent = Demo_control.instance.currentActiveGameObject.transform;
                        return;
                    }

                   // if(currentSticker)  currentSticker.transform.position = hit.point;
                }
            }
        }
    }
    
    public static Texture2D TextureFromSprite(Sprite sprite)
    {
        
        int width = Mathf.CeilToInt(sprite.textureRect.width);
        int height = Mathf.CeilToInt(sprite.textureRect.height);
        
        Texture2D newText = new Texture2D(width,height);
        
        Color[] colors = newText.GetPixels();
        
        Color[] newColors = sprite.texture.GetPixels(Mathf.CeilToInt(sprite.textureRect.x),
            Mathf.CeilToInt(sprite.textureRect.y),
            width,
            height);
        
        Debug.Log(colors.Length+"_"+ newColors.Length);
        newText.SetPixels(newColors);
        newText.Apply();
        return newText;
    }
}
