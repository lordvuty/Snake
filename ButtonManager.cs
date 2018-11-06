using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Button start;
    public Button exit;

    void Start() {

        PlayerPrefs.SetInt("lives", 3);

        Button btn1 = start.GetComponent<Button>();
        Button btn2 = exit.GetComponent<Button>();

        btn1.onClick.AddListener(OnClickStart);
        btn2.onClick.AddListener(OnClickExit);

    }

    public void OnClickStart() {

        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void OnClickExit() {

        Application.Quit();
    }
}