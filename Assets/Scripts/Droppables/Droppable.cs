using System;
using UnityEngine;

namespace Droppables
{
    public abstract class Droppable : MonoBehaviour
    {
        [SerializeField] protected DroppableType droppableType;
        
        protected Transform NextParent;
        protected Animator Animator;
        protected Rigidbody2D Rigidbody;

        public DroppableType Type => droppableType;

        protected void Awake ()
        {
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody2D>();
            
            NextParent = GameObject.Find("Droppables").transform;
        }

        public void Drop ()
        {
            transform.parent = NextParent;
            Rigidbody.isKinematic = false;
            Rigidbody.gravityScale = 5f;
        }

        protected void LatchOntoGround ()
        {
            Rigidbody.isKinematic = true;
        }

        public virtual void SetHome () {}

        public virtual void SetAway () {}
    }
}