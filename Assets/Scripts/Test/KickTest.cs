using Core;
using UnityEngine;

namespace Test
{
    public class KickTest : MonoBehaviour
    {
        [SerializeField] private bool _startedClicking = false;

        private Vector3 _startPosition;
    
        private void Update ()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitPlane;
                Ray startRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(startRay, out hitPlane))
                {
                    _startPosition = hitPlane.point;
                    _startedClicking = true;
                    Debug.DrawLine(_startPosition, _startPosition + Vector3.up, Color.magenta, 2.5f);
                }
            }
        
            if (Input.GetMouseButtonUp(0))
            {
                _startedClicking = false;
            }

            if (_startedClicking)
            {
                RaycastHit hitBall;
                Ray endRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(endRay, out hitBall))
                {
                    if (hitBall.collider.CompareTag("Ball"))
                    {
                        Debug.DrawLine(_startPosition, hitBall.point, Color.cyan, 2.5f);
                        Vector3 kickDirection = hitBall.point - _startPosition;
                        hitBall.collider.GetComponent<Ball>().BallMove(kickDirection.normalized, 5f);
                        _startedClicking = false;
                    }
                }
            }
        }
    }
}
