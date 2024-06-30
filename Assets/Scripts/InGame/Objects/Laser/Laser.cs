using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject laserBody;
    [SerializeField] float laserLength = 10f;
    float laserRadius = 0.2f;
    RaycastHit2D raycastHit; // 레이저가 닿은 물체
    Vector3 laserDirection; // 레이저의 방향
    Vector2 originalScale;
    private void Awake()
    {
        laserDirection = (gameObject.transform.position - laserBody.transform.position).normalized;

        //레이저와 몸통의 방향에 따라서 스케일을 조정
        if (laserDirection.x == 0)
            originalScale = new Vector2(laserRadius + 0.1f, laserLength);
        if (laserDirection.y == 0)
            originalScale = new Vector2(laserLength, laserRadius + 0.1f);
    }

    private void Update()
    {
        raycastHit = Physics2D.CircleCast(gameObject.transform.position, laserRadius, laserDirection, laserLength);
        Debug.DrawRay(gameObject.transform.position, laserDirection * laserLength, Color.red);
        if (raycastHit.collider != null) // 물체가 닿아있을때
        {
            if (raycastHit.transform.CompareTag("Player")) // 게임 오버 판정
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (laserDirection.x == 0) // 레이저가 세로일때
            {
                transform.localScale = new Vector2(originalScale.x, Mathf.Abs(raycastHit.point.y - gameObject.transform.position.y));
            }
            else if (laserDirection.y == 0) // 레이저가 가로일때
            {
                transform.localScale = new Vector2(Mathf.Abs(raycastHit.point.x - gameObject.transform.position.x), originalScale.y);
            }
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
}
