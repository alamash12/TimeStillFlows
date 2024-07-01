using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
* StageSelect scene에서의 상호작용
*/
public class StageSelect : MonoBehaviour
{
    public void RetToMain() // 뒤로가기 버튼
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Start()
    {
        for(int i=0; i<8; i++)
        {
            string stageName = "Stage_0" + (i + 1);

            if (PlayerPrefs.GetInt(stageName) == 1)
            {
                var image = transform.GetChild(i).GetComponent<Image>();
                Color color = image.color;
                color.a = 1f;
                image.color = color;
            }
        }
    }
    public void StageBtn1()
    {
        if (PlayerPrefs.GetInt("Stage_01") == 1) SceneManager.LoadScene("Stage_01");
        SoundManager.Instance.WoodBgmOn();
    }
    public void StageBtn2()
    {
        if (PlayerPrefs.GetInt("Stage_02") == 1) SceneManager.LoadScene("Stage_02");
        SoundManager.Instance.WoodBgmOn();
    }
    public void StageBtn3()
    {
        if (PlayerPrefs.GetInt("Stage_03") == 1) SceneManager.LoadScene("Stage_03");
        SoundManager.Instance.WoodBgmOn();
    }
    public void StageBtn4()
    {
        if (PlayerPrefs.GetInt("Stage_04") == 1) SceneManager.LoadScene("Stage_04");
        SoundManager.Instance.WoodBgmOn();
    }
    public void StageBtn5()
    {
        if (PlayerPrefs.GetInt("Stage_05") == 1) SceneManager.LoadScene("Stage_05");
        SoundManager.Instance.TownBgmOn();
    }
    public void StageBtn6()
    {
        if (PlayerPrefs.GetInt("Stage_06") == 1) SceneManager.LoadScene("Stage_06");
        SoundManager.Instance.TownBgmOn();
    }
    public void StageBtn7()
    {
        if (PlayerPrefs.GetInt("Stage_07") == 1) SceneManager.LoadScene("Stage_07");
        SoundManager.Instance.TownBgmOn();
    }
    public void StageBtn8()
    {
        if (PlayerPrefs.GetInt("Stage_08") == 1) SceneManager.LoadScene("Stage_08");
        SoundManager.Instance.TownBgmOn();
    }
}

