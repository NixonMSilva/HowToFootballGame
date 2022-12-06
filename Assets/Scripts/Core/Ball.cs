using System;
using UnityEngine;

namespace Core
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private void Awake ()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void BallMove (Vector2 direction, float kickStrength)
        {
            _rigidbody.AddForce(direction * kickStrength, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D (Collision2D col)
        {
            switch (col.gameObject.tag)
            {
                case "Crossbar":
                    GameManager.Instance.OnCrossbar?.Invoke();
                    break;
                case "Player":
                    GameManager.Instance.OnKick?.Invoke();
                    break;
                default:
                    break;
            }
        }
    }
}
