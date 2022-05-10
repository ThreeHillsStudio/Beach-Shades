using System.Collections;
using System.Collections.Generic;
using EasyGameStudio.paint;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform start;
    public Transform end;
    public Transform buy;

    private float y;
    private Animator _animator;
    private Vector3 _startPosition;

    public List<GameObject> helloMessages;
    public List<GameObject> reactionMessages;

    public GameObject selectBar;
    public GameObject glassesGroup;

    public GameObject startMenu;

    public GameObject eyes;

    public static MovePlayer instance;

    public GameObject title;
    public GameObject progressBar;

    public GameObject panel;

    private bool _isGameStarted;

    void Start()
    {
        instance = this;
        _animator = GetComponent<Animator>();
        title.SetActive(true);
        progressBar.SetActive(false);
        glassesGroup.SetActive(false);
        selectBar.SetActive(false);
        startMenu.SetActive(true);

    }

    public void OnStartGame()
    {
        if (_isGameStarted) return;

        _isGameStarted = true;
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        title.SetActive(false);
        progressBar.SetActive(true);
        y = transform.position.y;
        start.position = new Vector3(start.position.x, y, start.position.z);
        buy.position = new Vector3(buy.position.x, y, buy.position.z);
        end.position = new Vector3(end.position.x, y, end.position.z);

        selectBar.SetActive(false);
        glassesGroup.SetActive(false);
        startMenu.SetActive(false);
        transform.position = start.position;
        _startPosition = start.position;
        _animator.Play("move");
        StartCoroutine(MoveToBuy(2));
    }

  public IEnumerator UseGlassesAndGo()
    {
        glassesGroup.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        selectBar.SetActive(false);
        
        glassesGroup.transform.parent = eyes.transform;
        //eyes.transform.position = Vector3.zero;
        // eyes.transform.rotation = Quaternion.identity;
        //eyes.transform.localScale = Vector3.one;
        glassesGroup.transform.localPosition = new Vector3(-0.004f, 0.17f, -0.085f);
        glassesGroup.transform.rotation = Quaternion.identity;
        Demo_control.instance.currentActiveGameObject.transform.rotation = Demo_control.instance.startRotation;
        
        var reactionMsg = reactionMessages[Random.Range(0, reactionMessages.Count)];
        reactionMsg.SetActive(true);
        _animator.Play("Clap");
        yield return new WaitForSeconds(3f);

        _startPosition = transform.position;
        reactionMsg.SetActive(false);
        //_animator.enabled = true;

        var fov = Camera.main.fieldOfView;
        Camera.main.fieldOfView = 10;
        yield return new WaitForSeconds(3f);
        Camera.main.fieldOfView = fov;

        _animator.Play("move");
        for (var i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(3 / 100f);

            transform.position = Vector3.Lerp(_startPosition, end.position, i / 100f);

            transform.LookAt(end.position + Vector3.forward * 10);
        }

        yield return new WaitForSeconds(1f);

        ReloadScene();

        yield break;
    }
    public IEnumerator MoveToBuy(float time)
    {
        for(var i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(time / 100f);

            transform.position =  Vector3.Lerp(_startPosition, buy.position, i / 100f);

            transform.LookAt(buy.position + Vector3.forward * 10);
        }

        //_animator.enabled = false;
        _animator.Play("Idle");
        var helloMsg = helloMessages[Random.Range(0, helloMessages.Count)];
        helloMsg.SetActive(true);
        yield return new WaitForSeconds(1f);
        helloMsg.SetActive(false);

        selectBar.SetActive(true);
        glassesGroup.SetActive(true);
        // transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, -180, transform.rotation.eulerAngles.z);
    }

    private void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);

    }
}
