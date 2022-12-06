using UnityEngine;

namespace Core
{
    public class Goal : MonoBehaviour
    {
        [SerializeField] private MatchSide goalSide;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!GameManager.Instance.gameIsOn)
                return;
            
            if (col.gameObject.CompareTag("Ball"))
            {
                GameManager.Instance.OnScoreNoCallback?.Invoke(goalSide);
                GameManager.Instance.OnScore?.Invoke(goalSide, GameManager.Instance.OnClawsStarted);
            }
        }
    }
}
