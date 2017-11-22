
using UnityEngine;
using UnityEngine.UI;

public class ToMainSceneScript : MonoBehaviour
{
    public void ToMain()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("main");
    }
}