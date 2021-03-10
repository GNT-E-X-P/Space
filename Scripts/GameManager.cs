using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum Stage
    {
        One,
        Two,
        Thr
    }

    public Stage stage;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        stage = Stage.One;
    }

    public void LoadScene(Stage stage)
    {
        switch (stage)
        {
            case Stage.One:
                SceneManager.LoadScene(1);
                break;
            case Stage.Two:
                SceneManager.LoadScene(2);
                break;
        }
    }
}
