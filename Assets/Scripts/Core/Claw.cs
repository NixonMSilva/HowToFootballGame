using System;
using DG.Tweening;
using Droppables;
using UnityEngine;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;

namespace Core
{
    public class Claw : MonoBehaviour
    {
        [SerializeField] private MatchSide clawOwner;

        [SerializeField] private float clawSpeed = 15f;

        [SerializeField] private Transform limitLeft, limitRight;

        private Droppable _drop = null;
        
        private Sequence _clawSequence;

        private Rigidbody2D _rigidbody;

        private bool _hasReleased = false;

        private void Awake ()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void OnEnable ()
        {
            _clawSequence = DOTween.Sequence().SetAutoKill(false);
            switch (clawOwner)
            {
                case MatchSide.Home:
                    _clawSequence.Append(_rigidbody.DOMoveX(limitRight.position.x, clawSpeed).SetEase(Ease.Linear));
                    _clawSequence.Append(_rigidbody.DOMoveX(limitLeft.position.x, clawSpeed).SetEase(Ease.Linear));
                    break;
                case MatchSide.Away:
                    _clawSequence.Append(_rigidbody.DOMoveX(limitLeft.position.x, clawSpeed).SetEase(Ease.Linear));
                    _clawSequence.Append(_rigidbody.DOMoveX(limitRight.position.x, clawSpeed).SetEase(Ease.Linear));
                    break;
                default:
                    break;
            }
            _clawSequence.SetLoops(-1, LoopType.Restart);
            _clawSequence.SetSpeedBased(true);
            _clawSequence.Play();
        }

        private void Update ()
        {
            if (GameManager.Instance.gameIsOn)
            {
                return;
            }

            switch (clawOwner)
            {
                case MatchSide.Home:
                    ProcessClawHome();
                    break;
                case MatchSide.Away:
                    ProcessClawAway();
                    break;
                default:
                    break;
            }
        }

        private void ProcessClawHome ()
        {
            if (Input.GetKeyDown(KeyCode.E) && !_hasReleased)
                ReleaseClaw();
        }

        private void ProcessClawAway ()
        {
            if (Input.GetKeyDown(KeyCode.Insert) && !_hasReleased)
                ReleaseClaw();
        }

        private void ReleaseClaw ()
        {
            GameManager.Instance.OnClawReleased?.Invoke();
            GameManager.Instance.OnDroppableDrop?.Invoke(_drop.Type);
            _clawSequence.Kill();
            _drop.Drop();
            _drop = null;
            _hasReleased = true;
        }

        public void ResetRelease () => _hasReleased = false;

        public void SetDrop (Droppable drop) => _drop = drop;
    }
}
