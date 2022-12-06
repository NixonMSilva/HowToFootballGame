using UnityEngine;

namespace Droppables
{
    public class Slime : Droppable
    {
        private bool _canStick = false;
        
        private static readonly int HasLatched = Animator.StringToHash("hasLatched");

        public new void Drop ()
        {
            base.Drop();
            Animator.Play("fall");
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
            if (!_canStick)
                return;
            
            var component = col.gameObject.GetComponent<Rigidbody2D>();
            if (col.CompareTag("Player") || col.CompareTag("Ball"))
            {
                component.gravityScale = 50f;
                component.velocity = Vector2.zero;
            }
        }
        
        private void OnTriggerExit2D (Collider2D col)
        {
            if (!_canStick)
                return;

            var component = col.gameObject.GetComponent<Rigidbody2D>();
            if (col.CompareTag("Player"))
            {
                component.gravityScale = 5f;
            }
            else if (col.CompareTag("Ball"))
            {
                component.gravityScale = 1f;
            }
        }

        private new void LatchOntoGround ()
        {
            base.LatchOntoGround();
            Animator.SetTrigger(HasLatched);
            _canStick = true;
        }
    }
}