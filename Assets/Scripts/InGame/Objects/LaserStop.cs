using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserStop : MonoBehaviour
{
    [SerializeField] float laserRadius = 0.3f;
    [SerializeField] float laserLength = 10f;
    RaycastHit2D raycastHit; // 레이저가 닿은 물체
    Vector3 parentPosition; // 레이저 몸통의 포지션
    Vector3 laserDirection; // 레이저의 방향
    Vector2 originalScale;
    private void Awake()
    {
        parentPosition = gameObject.transform.parent.position;
        laserDirection = (gameObject.transform.position - parentPosition).normalized;
        originalScale = transform.localScale;
    }

    private void Update()
    {
        raycastHit = Physics2D.CircleCast(gameObject.transform.position, laserRadius, laserDirection, laserLength);
        Debug.DrawRay(gameObject.transform.position, laserDirection * laserLength, Color.red);
        if (raycastHit.collider != null)
        {
            if (raycastHit.transform.CompareTag("Object"))
            {
                if (laserDirection.x == 0)
                {
                    transform.localScale = new Vector2(originalScale.x, Mathf.Abs(raycastHit.point.y - gameObject.transform.position.y));
                }
                else if (laserDirection.y == 0)
                {
                    transform.localScale = new Vector2(Mathf.Abs(raycastHit.point.x - gameObject.transform.position.x), originalScale.y);
                }
            }
            else if (raycastHit.transform.CompareTag("Player"))
                Debug.Log("Game Over");
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
}
