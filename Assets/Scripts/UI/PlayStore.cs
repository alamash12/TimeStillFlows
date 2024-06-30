using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStore : MonoBehaviour
{
    // Start is called before the first frame update
    public void OpenPlayStore()
    {
        string appPackageName = "com.ExpMake.TimeStillFlow"; // 앱의 패키지 이름
        Application.OpenURL($"market://details?id={appPackageName}");
    }
}
