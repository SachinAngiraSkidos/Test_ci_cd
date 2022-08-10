using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject GameoverUI;
    [SerializeField]
    private Text GameoverText;

    public static UIController Instance;


    private void Start()
    {
        Instance = this;
    }

    public void SetGameOverText(string s)
    {
        GameoverText.text = s;
        Time.timeScale = 0f;
        GameoverUI.SetActive(true);
    }
    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
