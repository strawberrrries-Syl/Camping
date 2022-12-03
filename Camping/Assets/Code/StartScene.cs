using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public static StartScene Singleton;
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        Singleton = this;
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
            startGameInternal("map1");
    }

    public static void setStatus() {
        Singleton.setStatusInternal(); 
    }

    public void startGameInternal(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    private void setStatusInternal() {
        start = true;
    }
}
