using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilhouetteButtonTest : MonoBehaviour
{
    Player player; 
    bool isSilhouette = false;
    Vector3 playerLocation;

    // Start is called before the first frame update
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SilhouetteClicked);
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void SilhouetteClicked()
    {
        if (!isSilhouette)
        {
            playerLocation = player.GetLocation();
            isSilhouette = true;    
        }

        else //실루엣이 형성되어있는 상태. 플레이어를 이동시킴.
        {
            player.SetLocation(playerLocation);
            isSilhouette = false;
        }
    }
}
