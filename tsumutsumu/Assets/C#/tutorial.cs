using UnityEngine;
using System.Collections;

public class tutorial : MonoBehaviour {
    public void main_tutorial()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("tutorial0");
    }
    public void tutorial0()
    {
        Application.LoadLevel("tutorial1");
        GetComponent<AudioSource>().Play();
        
    }
    public void tutorial1()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("tutorial2");
    }
    public void tutorial2()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("tutorial3");
    }
    public void tutorial3()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("tutorial4");
    }
    public void tutorial4()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("tutorial5");
    }
    public void tutorial5()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("tutorial6");
    }
    public void tutorial6()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("tutorial7");
    }
    public void tutorial7()
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel("title1");
    }
}
