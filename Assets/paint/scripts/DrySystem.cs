using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DrySystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leftLight;
    public GameObject rightLight;
    
    public GameObject leftLight1;
    public GameObject rightLight1;

    public GameObject nextButton;

    private Slider _slider;
    private Light _leftLight;
    private Light _rightLight;
    
    private Light _leftLight1;
    private Light _rightLight1;
    

    private float _progress;
    private float _maxTime = 2f;

    public GameObject progressSlider;
    private Slider _progressSlider;

    public static DrySystem instance;

    private bool _isFocus;

    private bool _isFinished;
    void Start()
    {
        instance = this;

        _slider = GetComponent<Slider>();
        _leftLight = leftLight.GetComponent<Light>();
        _rightLight = rightLight.GetComponent<Light>();
        
        _leftLight1 = leftLight1.GetComponent<Light>();
        _rightLight1 = rightLight1.GetComponent<Light>();

        _leftLight.intensity = _slider.value;
        _rightLight.intensity = _slider.value;
        
        _leftLight1.intensity = _slider.value;
        _rightLight1.intensity = _slider.value;
        nextButton.SetActive(false);

        _progressSlider = progressSlider.GetComponent<Slider>();
    }

    public void OnStart()
    {
        _slider.value = 0;
        _progressSlider.value = 0;
        _progress = 0;
        nextButton.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFocus) _progress += (Time.deltaTime / _maxTime) * _slider.value;
        if (!_isFocus)
        {
            if(_slider.value > 0) _slider.value -= 0.1f;
            if (_slider.value < 0) _slider.value = 0;
        }
        
        if (_slider.value > 0.68) _slider.value = 0.68f;
            
        if(_progress > _maxTime)
        {
            _progress = _maxTime;
            nextButton?.SetActive(true);
            SceneController.instance.AudioSource.volume = 1;
            SceneController.instance.AudioSource.Stop();
            _isFinished = true;
            return;
        }

        _progressSlider.value = _progress / _maxTime;

        //Debug.Log(_progress);
        _leftLight.intensity = _slider.value;
        _rightLight.intensity = _slider.value;
        
        _leftLight1.intensity = _slider.value;
        _rightLight1.intensity = _slider.value;

        SceneController.instance.AudioSource.volume = 0.5f + _slider.value;
    }

    public void SetIsFocus(bool focus)
    {
        _isFocus = focus;
        if(_isFinished == false) SceneController.instance.AudioSource.PlayOneShot(SceneController.instance.heatSound);
    }
}
