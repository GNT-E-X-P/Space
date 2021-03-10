using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetController : MonoBehaviour
{
    public Vector3 spinSpeed;
    private GameManager gameM;

    private void Awake()
    {
        gameM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(spinSpeed.x, spinSpeed.y, spinSpeed.z) * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameM.LoadScene(GameManager.Stage.One);
        }
    }
}
