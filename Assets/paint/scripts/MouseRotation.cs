using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using EasyGameStudio.paint;
using UnityEngine.UI;

public class MouseRotation : MonoBehaviour
{

    private Touch touch;
    private Vector2 oldTouchPosition;
    private Vector2 NewTouchPosition;
    [SerializeField]
    private float keepRotateSpeed = 10f;

    [HideInInspector]
    public bool isOverCylinder;

    public static MouseRotation instance;
    private Vector3 pos;

    public GameObject sliderGameObject;
    private Slider _slider;
    public void Start()
    {
        instance = this;
        _slider = sliderGameObject.GetComponent<Slider>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, pos);
    }

    private void Update()
    {

        if (!Spray.instance.isSprayActive && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
           // Spray.instance.isSprayActive = true;
           pos = Input.mousePosition;
           pos.x = 0;
           pos.y = 0;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "cylinder")
                {
                    RotateThings();
                    isOverCylinder = true;

                    return;
                }

                isOverCylinder = false;
            }
        }
            
    }
    private void RotateThings()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                oldTouchPosition = touch.position;
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                NewTouchPosition = touch.position;
            }

            Vector2 rotDirection = oldTouchPosition - NewTouchPosition;
            Debug.Log(rotDirection);
            if (rotDirection.x < 0)
            {
                RotateRight();
            }

            else if (rotDirection.x > 0)
            {
                RotateLeft();
            }
        }
    }

    void RotateLeft()
    {
        transform.rotation = Quaternion.Euler(0f, 1.5f * keepRotateSpeed, 0f) * transform.rotation;
        var rot = Demo_control.instance.currentActiveGameObject.transform.rotation;
        Demo_control.instance.currentActiveGameObject.transform.Rotate(Vector3.forward, 1.5f * keepRotateSpeed);
    }

    void RotateRight()
    {
        transform.rotation = Quaternion.Euler(0f, -1.5f * keepRotateSpeed, 0f) * transform.rotation;
        Demo_control.instance.currentActiveGameObject.transform.Rotate(Vector3.forward, -1.5f * keepRotateSpeed);

    }

    public void SetRotation()
    {
        var angle = _slider.value > 0.5f ? (_slider.value - 0.5f) / 0.5f / 3f : (0.5f -_slider.value) / 0.5f / 3f * -1;
        transform.rotation = Quaternion.Euler(90f, angle * 360, 0f);
        Demo_control.instance.currentActiveGameObject.transform.rotation = 
            Quaternion.Euler(
                Demo_control.instance.currentActiveGameObject.transform.rotation.eulerAngles.x, 
                angle * 360,
                Demo_control.instance.currentActiveGameObject.transform.rotation.eulerAngles.z
                );
    }

}
