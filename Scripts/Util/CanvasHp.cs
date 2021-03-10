using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHp : MonoBehaviour
{
    public Text Now_HP_Text;
    public Slider Slider_Value;

    private Player playerScript;

    private float NOW_HP;
    private float MAX_HP;
    // Start is called before the first frame update

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();

        MAX_HP = playerScript.maxHp;
        NOW_HP = playerScript.nowHp;

        Now_HP_Text.text = "HP : " + NOW_HP + " / " + MAX_HP;

    }
    // Update is called once per frame
    void Update()
    {
        NOW_HP = playerScript.nowHp;
        Slider_Value.value = NOW_HP;
        Now_HP_Text.text = "HP : " + NOW_HP + " / " + MAX_HP;
    }
}
