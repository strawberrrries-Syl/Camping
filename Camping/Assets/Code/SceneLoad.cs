using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public static SceneLoad Singleton;

    public bool MMArrive;
    public bool KBArrive;

    // Start is called before the first frame update
    void Start()
    {
        MMArrive = false;
        KBArrive = false;
        Singleton = this;
    }

    private void Update()
    {
        if (MMArrive && KBArrive)
        {
            if (SceneManager.GetActiveScene().name == "map1")
            {
                if (ScoreKeeper.GetScore() >= 24)
                    LoadScene("map2");
                else {
                    LoadScene("map1");
                }
            } 

            if (SceneManager.GetActiveScene().name == "map2")
            {
                if (ScoreKeeper.GetScore() >= 24)
                    LoadScene("End");
                else 
                    LoadScene("map2");
            }
        }

        if (SceneManager.GetActiveScene().name == "End")
        {
            Debug.Log("end scene");
            if (Input.GetKeyDown("r"))
            {
                Debug.Log("pressed R");
                LoadScene("Start");
            }

        }
    }

    public static void SetStatus(int type) {
        Singleton.SetStatusInternal(type);
    }

    public static void resetScene() {
        if (SceneManager.GetActiveScene().name == "map1")
        {
            SceneManager.LoadScene("map1");
        }
        if (SceneManager.GetActiveScene().name == "map2")
        {
            SceneManager.LoadScene("map2");
        }
    }

    private void SetStatusInternal(int type) { 
        if(type == 1 && !MMArrive)
            MMArrive = true;
        if (type == 2 && !KBArrive)
            KBArrive = true;
    }


    public void LoadScene(string sceneName)
    {
        MMArrive = false;
        KBArrive = false;
        SceneManager.LoadScene(sceneName);
    }
}
