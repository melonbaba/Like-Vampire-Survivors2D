using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()//데이터 갱신은 대부분 Update에서 하기에, 갱신된 이후 UI를 변화시키기 위함.
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.Ins.exp;
                float maxExp = GameManager.Ins.nextexp[GameManager.Ins.level];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.Ins.level);//F0, F1, F2 : 소수점 자리를 지정. F0 : 소수점 X
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.Ins.kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.Ins.maxGameTime - GameManager.Ins.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);//D0, D1, D2 : 자리 수를 지정. D2 : 두 자리수 지정.
                break;
            case InfoType.Health:
                float curHealth = GameManager.Ins.heatlh;
                float maxHealth = GameManager.Ins.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }


}
