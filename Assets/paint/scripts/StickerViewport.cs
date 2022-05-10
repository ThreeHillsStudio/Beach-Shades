using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyGameStudio.paint;

public class StickerViewport : MonoBehaviour
{
    public GameObject stickerBar;
    public GameObject nextButton;
    public GameObject backButton;
    public GameObject canvas;

    public List<GameObject> views;

    public Transform cameraPosition;

    public static StickerViewport instanace;

    public GameObject sunglassesGroup;

    // Start is called before the first frame update
    void Start()
    {
        instanace = this;

        backButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            StartScene();

        });
    }

    public void StartScene()
    {
        Camera.main.transform.position = cameraPosition.position;
        Camera.main.transform.LookAt(Camera.main.transform.position + Vector3.forward);
        sunglassesGroup.transform.rotation = Quaternion.Euler(0, 0, 0);
        Demo_control.instance.currentActiveGameObject.transform.rotation = Quaternion.Euler(90,0,65);
        sunglassesGroup.transform.position = new Vector3(-40.3199997f, 0.540000021f, 4f);
        SceneController.instance.isCameraZoomed = false;
        
        stickerBar.SetActive(false);
        nextButton.SetActive(true);
        canvas.SetActive(true);
        backButton.SetActive(false) ;
    }

    public void MiniScene()
    {

        //sunglassesGroup.transform.position = new Vector3(-41, 0, 0);
        SceneController.instance.isCameraZoomed = true;
        Demo_control.instance.currentActiveGameObject.transform.rotation = Quaternion.Euler(90,0,0);
        stickerBar.SetActive(true);
        nextButton.SetActive(false);
        canvas.SetActive(false);
        backButton.SetActive(true);
    }

    public void OnViewClick(int view)
    {
        MiniScene();
        

        
        if (view == 0 || view == 1)
        {
           
            sunglassesGroup.transform.rotation = Quaternion.Euler(0, 0, 0);
            //Camera.main.transform.LookAt(Camera.main.transform.position + Vector3.forward);
        }

        if (view == 2)
        {
            sunglassesGroup.transform.rotation = Quaternion.Euler(0, -90, 0);
            //Camera.main.transform.LookAt(Camera.main.transform.position + Vector3.right);
        }

        if (view == 3)
        {
            sunglassesGroup.transform.rotation = Quaternion.Euler(0, 90, 0);
            //Camera.main.transform.LookAt(Camera.main.transform.position + Vector3.left);
        }

      

        Camera.main.transform.position = views[view].transform.position;
        Camera.main.transform.LookAt(Camera.main.transform.position + Vector3.forward);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
