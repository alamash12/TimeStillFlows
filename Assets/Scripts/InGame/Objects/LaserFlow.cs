using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFlow : MonoBehaviour
{
    [SerializeField] float laserRadius = 0.5f;
    [SerializeField] float laserLength = 10f;
    LineRenderer lineRenderer;
    RaycastHit2D raycastHit; // 레이저가 닿은 물체
    Vector3 parentPosition; // 레이저 몸통의 포지션
    Vector3 laserDirection; // 레이저의 방향
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.startWidth = laserRadius;
        lineRenderer.endWidth = laserRadius;
        parentPosition = gameObject.transform.parent.position;
        laserDirection = (gameObject.transform.position - parentPosition).normalized;
    }

    private void Update()
    {
        raycastHit = Physics2D.CircleCast(gameObject.transform.position, laserRadius, laserDirection, laserLength); // 두께가 있는 raycast
        if (raycastHit.collider != null) // 물체가 닿아있을때
        {
            if (laserDirection.x == 0) // 레이저가 세로일때
            {
                lineRenderer.SetPosition(1, new Vector2(parentPosition.x, raycastHit.point.y));
            }
            else if (laserDirection.y == 0) // 레이저가 가로일때
            {
                lineRenderer.SetPosition(1, new Vector2(raycastHit.point.x, parentPosition.y));
            }
            if(raycastHit.collider.gameObject.CompareTag("Player"))
            {
                //게임오버
            }
        }
        else // 물체가 닿지 않았을때
        {
            lineRenderer.SetPosition(1, gameObject.transform.position + laserDirection * laserLength);
        }
    }
}
