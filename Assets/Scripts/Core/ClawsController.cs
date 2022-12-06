using System;
using Droppables;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Core
{
    public class ClawsController : MonoBehaviour
    {
        [Header("Claw Properties")]
        [Space]
        [SerializeField] private Claw clawHome, clawAway;
        [SerializeField] private Transform clawHomeStartingPosition, clawAwayStartingPosition;
        [SerializeField] private Rigidbody2D clawHomeRigidBody, clawAwayRigidBody;

        [Header("Droppable Properties")]
        [Space]
        [SerializeField] private GameObject[] dropList;
        [SerializeField] private Transform clawHomeDropStart, clawAwayDropStart;

        private int _clawsReleased = 0;

        private void OnEnable ()
        {
            GameManager.Instance.OnClawsStarted += InitiateClaws;
            GameManager.Instance.OnClawReleased += CountClaw;
        }

        private void OnDisable ()
        {
            GameManager.Instance.OnClawsStarted -= InitiateClaws;
            GameManager.Instance.OnClawReleased -= CountClaw;
        }

        private void InitiateClaws ()
        {
            clawHome.gameObject.SetActive(true);
            clawAway.gameObject.SetActive(true);

            var homeDropSpawn = Random.Range(0, dropList.Length);
            var awayDropSpawn = Random.Range(0, dropList.Length);

            var homeDrop = Instantiate(dropList[homeDropSpawn], clawHomeDropStart).GetComponent<Droppable>();
            var awayDrop = Instantiate(dropList[awayDropSpawn], clawAwayDropStart).GetComponent<Droppable>();
            
            homeDrop.SetHome();
            awayDrop.SetAway();

            clawHome.SetDrop(homeDrop);
            clawAway.SetDrop(awayDrop);
        }

        private void CountClaw ()
        {
            _clawsReleased++;
            if (_clawsReleased == 2)
            {
                _clawsReleased = 0;
                
                // Game re-starts
                GameManager.Instance.OnGameReady?.Invoke();
                GameManager.Instance.gameIsOn = true;
                
                Invoke(nameof(HideClaws), 1f);
            }
        }

        private void HideClaws ()
        {
            ResetClawsPosition ();
            clawHome.gameObject.SetActive(false);
            clawAway.gameObject.SetActive(false);
        }

        private void ResetClawsPosition ()
        {
            clawHomeRigidBody.isKinematic = true;
            clawAwayRigidBody.isKinematic = true;
            clawHome.transform.position = clawHomeStartingPosition.position;
            clawAway.transform.position = clawAwayStartingPosition.position;
            clawHomeRigidBody.isKinematic = false;
            clawAwayRigidBody.isKinematic = false;
            clawHome.ResetRelease();
            clawAway.ResetRelease();
        }
    }
}