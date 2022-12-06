using System;
using UnityEngine;

namespace Droppables
{
    public class Propeller : Droppable
    {
        [SerializeField] private Vector2 propelDirection = Vector2.right;
        [SerializeField] private float propelStrength = 15f;

        private bool _canPropel = false;
        
        private static readonly int HasLatched = Animator.StringToHash("hasLatched");

        private new void LatchOntoGround ()
        {
            base.LatchOntoGround();
            Animator.SetBool(HasLatched, true);
            _canPropel = true;
        }
        
        private void OnCollisionEnter2D (Collision2D col)
        {
            if (col.gameObject.CompareTag("Pitch"))
            {
                LatchOntoGround();
            }
        }

        private void OnTriggerEnter2D (Collider2D col)
        {
            if (!_canPropel)
                return;
            
            if (col.CompareTag("Player") || col.CompareTag("Ball"))
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(propelDirection * propelStrength, ForceMode2D.Impulse);
            }
        }

        public void SetPropelDirection (Vector2 direction)
        {
            propelDirection = direction;
        }

        public override void SetHome () => propelDirection = Vector2.right;
        
        public override void SetAway () => propelDirection = Vector2.left;
    }
}