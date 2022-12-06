using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Droppables
{
    public class IceBlock : Droppable
    {
        [SerializeField] private int maxHealth = 3;
        private int _health;
        
        [SerializeField] private float damageCooldown = 0.3f;
        private float _damageCooldownElapsed = 0f;
        private bool _canBeDamaged = true;

        [SerializeField] private Vector2 offsetOnArm;

        private static readonly int HealthPercentage = Animator.StringToHash("HealthPercentage");

        private new void Awake ()
        {
            base.Awake();
            _health = maxHealth;
            Animator.SetFloat(HealthPercentage, ((float) _health / (float) maxHealth));
        }

        private void Start ()
        {
            transform.localPosition += (Vector3) offsetOnArm;
        }

        private void Update ()
        {
            if (_canBeDamaged)
                return;

            _damageCooldownElapsed += Time.deltaTime;
            if (_damageCooldownElapsed >= damageCooldown)
            {
                _damageCooldownElapsed = 0f;
                _canBeDamaged = true;
            }
        }

        private void OnCollisionEnter2D (Collision2D col)
        {
            if (col.gameObject.CompareTag("Pitch"))
            {
                LatchOntoGround();
            }
            
            if (col.gameObject.CompareTag("Ball") && _canBeDamaged)
            {
                DamageIce();
            }
        }

        private void DamageIce ()
        {
            _canBeDamaged = false;
            _health--;
            Animator.SetFloat(HealthPercentage, ((float) _health / (float) maxHealth));
            if (_health <= 0)
            {
                BreakIce();
            }
        }

        private void BreakIce ()
        {
            Destroy(gameObject, 0.1f);
        }
    }
}