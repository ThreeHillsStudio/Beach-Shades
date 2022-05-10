using System;
using System.Collections;
using System.Collections.Generic;
using EasyGameStudio.paint;
using UnityEngine;
using UnityEngine.UI;


public class SceneController : MonoBehaviour
{
    public List<GameObject> cameras;
    public List<Transform> postions;
    public GameObject sunglassesGroup;
    public List<GameObject> panels;
    
    public List<GameObject> progressList;

    [HideInInspector]
    public int currentScene = 0;
    private Camera _camera;

    public Sprite last;
    public Sprite current;
    public Sprite next;
    public static SceneController instance;

    public Vector3 lookAt;
    public Vector3 lookAt1;

    [HideInInspector]
    public Quaternion? lastCameraRotation;

    [HideInInspector] 
    public bool isCameraZoomed;

    private Vector3 _lastScale;
    
    public AudioSource AudioSource;
    public AudioClip clickSound;
    public AudioClip spraySound;
    public AudioClip heatSound;

    public AudioClip bgMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        instance = this;
        SwitchToScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Camera.main.transform.rotation = Quaternion.Euler(10f, 180, 0f);
        _camera.transform.LookAt(_camera.transform.position + Vector3.back * 5);
    }

    public void SwitchToScene(int index)
    {
        if(index >= 0 && index < cameras.Count - 1) currentScene = index;
        _camera.transform.position = cameras[index].transform.position;
        sunglassesGroup.transform.position = postions[index].position;
        SwitchToPanel(index);
        
        if(index == 0)
        {
            sunglassesGroup.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            progressList[0].GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
            progressList[0].GetComponent<Image>().sprite = current;
        }
    }

    public void SwitchToNextScene()
    {
        currentScene++;
        if (currentScene > cameras.Count - 1) currentScene = 0;
        _camera.transform.position = cameras[currentScene].transform.position;
        sunglassesGroup.transform.position = postions[currentScene].position;
        SwitchToPanel(currentScene);

        sunglassesGroup.transform.localScale = new Vector3(1, 1, 1);

        var pro = currentScene;
        if (currentScene == 0) pro = 4;
        for(var i = 0; i < pro; i++)
        {
            progressList[i].GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            progressList[i].GetComponent<Image>().sprite = last;
            
        }
        for (var i = pro + 1; i < 4; i++)
        {
            progressList[i].GetComponent<Image>().sprite = next;

        }
        if (pro < 4)
        {
            progressList[pro].GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
            progressList[pro].GetComponent<Image>().sprite = current;
        }
        
        if(currentScene == 0)
        {
            AudioSource.PlayOneShot(bgMusic);
            StickerViewport.instanace.canvas.SetActive(false);
            StartCoroutine( MovePlayer.instance.UseGlassesAndGo());
        }

        if (currentScene == 1)
        {
            Demo_control.instance.startRotation = Demo_control.instance.currentActiveGameObject.transform.rotation;
        }
        
        if (currentScene == 2)
        {
            var currentObject = Demo_control.instance.currentActiveGameObject;
            currentObject.transform.rotation = Demo_control.instance.startRotation;
            _lastScale = currentObject.transform.localScale;
            currentObject.transform.localScale /= 1.5f; // sunglassesGroup.transform.position = new Vector3(-0.0700000003f,0,1.25999999f);
            currentObject.GetComponent<Rotate_self>().is_auto_rotate = true;
            sunglassesGroup.transform.rotation = Quaternion.Euler(0, 65, 0);
            if(DrySystem.instance) DrySystem.instance.OnStart();
        }
        
        if (currentScene == 3)
        {
            var currentObject = Demo_control.instance.currentActiveGameObject;
            currentObject.transform.localScale = _lastScale;
            currentObject.GetComponent<Rotate_self>().is_auto_rotate = false;
            currentObject.transform.rotation = Demo_control.instance.startRotation;
            sunglassesGroup.transform.rotation = Quaternion.Euler(0, 70, 0);
            StickerViewport.instanace.StartScene();
           
        }
        
    }

    private void LateUpdate()
    {
        if (lastCameraRotation == null)
        {
            lastCameraRotation = _camera.transform.rotation;
        }
        if (currentScene == 3 && isCameraZoomed == false)
        {
            _camera.transform.eulerAngles = lookAt;
            return;
            
        }

        if (isCameraZoomed)
        {
            _camera.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        
        
        if (currentScene == 1)
        {
            _camera.fieldOfView = 29;
            _camera.transform.eulerAngles = lookAt1;
            return;
            
        }

        if(currentScene == 2 || currentScene == 3) _camera.fieldOfView = 35.6f;
    }

    private void SwitchToPanel(int index)
    {
        for(var i = 0; i < panels.Count; i++)
        {
            if (i == index)
            {
                panels[i].SetActive(true);
                continue;
            }

            panels[i].SetActive(false);
        }
    }
    
    public void click()
    {
        this.AudioSource.PlayOneShot(this.clickSound);
    }
}
