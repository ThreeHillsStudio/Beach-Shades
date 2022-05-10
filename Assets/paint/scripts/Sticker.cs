using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyGameStudio.paint;

public class Sticker : MonoBehaviour
{

    private Button _button;
    private Sprite _sticker;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _sticker = GetComponent<Image>().sprite;
        _button.onClick.AddListener(() =>
        {
            StickerChooser.instance.currentSticker = _sticker;
            Demo_control.instance.currentActiveGameObject.GetComponent<StickerPaint>().currentSticker = null;
            var stickers = StickerChooser.instance.stickers;
            foreach (var sticker in stickers)
            {
                if (sticker.Equals(gameObject))
                {
                    sticker.GetComponent<Button>().interactable = false;
                    //sticker.GetComponent<RectTransform>().sizeDelta = new Vector2(110,110);
                    continue;
                }
                sticker.GetComponent<Button>().interactable = true;
                //sticker.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
