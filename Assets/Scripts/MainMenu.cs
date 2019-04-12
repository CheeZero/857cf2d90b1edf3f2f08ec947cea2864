using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("PlayerRoadMap");
    }
    public void OnClickArena()
    {
        SceneManager.LoadScene("Arena");
    }
}
