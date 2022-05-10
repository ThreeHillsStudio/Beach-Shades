using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerChooser : MonoBehaviour
{

    public static StickerChooser instance;

    public List<GameObject> stickers;
    [HideInInspector]
    public Sprite currentSticker;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
