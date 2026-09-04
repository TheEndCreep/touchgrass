using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menü : MonoBehaviour
{
    public GameObject target;
    public GameObject target2;
    public GameObject Camera;

    public GameObject Titel;
    public GameObject Controls;
    public GameObject pressAnyKey;

    public string scenename;

    private bool isControlsActive = false;
    private bool isKeyPressedTwice = false;

    private bool keyPressed;
    private bool mousePressed;

    private float t = 0.0f;

    private Vector3 StartPosition;

    private Vector3 EndPosition = new Vector3(24.8f, 74.7f, -91.6f);

    private bool isMoving = false;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 0.5f;

    void Start()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.blocksRaycasts = false;
        }
    }
    void Update()
    {
        //spin the camera object
        if (!isControlsActive)
        {
            SpinObject(Camera, 10f);
        }

        //check if any key or mouse button is pressed
        bool keyPressed =
        Keyboard.current != null &&
        Keyboard.current.anyKey.wasPressedThisFrame;
        bool mousePressed =
        Mouse.current != null &&
        Mouse.current.leftButton.wasPressedThisFrame;

        bool anyInputPressed = keyPressed || mousePressed;

        if (isMoving == true)
        {
            t += 0.8f * Time.deltaTime;
            Camera.transform.position = Vector3.Lerp(StartPosition, EndPosition, t);
            if (t >= 1f)
            {
                Camera.transform.position = EndPosition;
                isMoving = false;
            }
        }

        if (isControlsActive && !isMoving)
        {
            SpinObjectTarget2(Camera, 10f);
        }



        //if user presses any key, change scene to the next one
        if (anyInputPressed && !isControlsActive && !isMoving)
        {
            Titel.SetActive(false);
            Controls.SetActive(true);
            pressAnyKey.SetActive(false);
            isControlsActive = true;

            StartPosition = Camera.transform.position;
            t = 0f;
            isMoving = true;
        }
        else if (isControlsActive)
        {
            if (anyInputPressed && !isKeyPressedTwice)
            {
                isKeyPressedTwice = true;
                StartCoroutine(Fade(1f));

            }
        }

    }

    //spin camera around target in this speed
    // camera will rotate around the target object at a speed of 10 degrees per second
    void SpinObject(GameObject obj, float speed)
    {
        obj.transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
    }

    void SpinObjectTarget2(GameObject obj, float speed)
    {
        obj.transform.RotateAround(target2.transform.position, Vector3.up, speed * Time.deltaTime);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeCanvasGroup == null)
            yield break;

        fadeCanvasGroup.blocksRaycasts = true;

        float startAlpha = fadeCanvasGroup.alpha;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;

        if (targetAlpha == 0f)
        {
            fadeCanvasGroup.blocksRaycasts = false;
        }
        yield return null;
        SceneManager.LoadScene(scenename);
    }

}