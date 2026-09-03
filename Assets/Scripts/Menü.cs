using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menü : MonoBehaviour
{
    public GameObject target;
    public GameObject Camera;

    public string scenename;

    void Update()
    {
        //spin the camera object
        SpinObject(Camera, 10f);

        //if user presses any key, change scene to the next one
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            SceneManager.LoadScene(scenename);
        }

    }

    //spin camera around target in this speed
    // camera will rotate around the target object at a speed of 10 degrees per second
    void SpinObject(GameObject obj, float speed)
    {
        obj.transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
    }

}