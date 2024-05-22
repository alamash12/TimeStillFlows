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

}
