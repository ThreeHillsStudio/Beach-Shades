using System.Collections;
using System.Collections.Generic;
using EasyGameStudio.paint;
using UnityEngine;

public class RotationButton : MonoBehaviour
{
    public void OnPointerLeftDown()
    {
      var currentGameObject = Demo_control.instance.currentActiveGameObject;
      currentGameObject.GetComponent<Rotate_self>().pointer_left_down();
    }
    
    public void OnPointerLeftUp()
    {
        var currentGameObject = Demo_control.instance.currentActiveGameObject;
        currentGameObject.GetComponent<Rotate_self>().pointer_left_up();
    }
    
    public void OnPointerRightDown()
    {
        var currentGameObject = Demo_control.instance.currentActiveGameObject;
        currentGameObject.GetComponent<Rotate_self>().pointer_right_down();
    }
    
    public void OnPointerRightUp()
    {
        var currentGameObject = Demo_control.instance.currentActiveGameObject;
        currentGameObject.GetComponent<Rotate_self>().pointer_right_up();
    }
}
