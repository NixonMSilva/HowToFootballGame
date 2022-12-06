using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Core
{
    public class MatchRestart : MonoBehaviour
    {
        [SerializeField] private Transform ballStartingPosition;
        [SerializeField] private Rigidbody2D ballRigidBody;

        [SerializeField] private Rigidbody2D homeOutfield;
        [SerializeField] private Rigidbody2D homeGoalkeeper;
        [SerializeField] private Transform homeOutfieldStartingPosition;
        [SerializeField] private Transform homeGoalkeeperStartingPosition;
        
        [SerializeField] private Rigidbody2D awayOutfield;
        [SerializeField] private Rigidbody2D awayGoalkeeper;
        [SerializeField] private Transform awayOutfieldStartingPosition;
        [SerializeField] private Transform awayGoalkeeperStartingPosition;
        
        private void OnEnable ()
        {
            GameManager.Instance.OnClawsStarted += ResetGame;
        }

        private void OnDisable ()
        {
            GameManager.Instance.OnClawsStarted -= ResetGame;
        }
        
        private void ResetGame ()
        {
            ResetBall();
            ResetPlayers();
        }

        private void ResetBall ()
        {
            ballRigidBody.isKinematic = true;
            ballRigidBody.velocity = Vector2.zero;
            ballRigidBody.transform.position = ballStartingPosition.position;
            ballRigidBody.isKinematic = false;
            ballRigidBody.velocity = Vector2.zero;
        }

        private void ResetPlayers ()
        {
            homeOutfield.isKinematic = true;
            homeOutfield.velocity = Vector2.zero;
            homeOutfield.transform.position = homeOutfieldStartingPosition.position;
            homeOutfield.transform.localScale = new Vector3(1f, 1f, 1f);
            homeOutfield.isKinematic = false;
            homeOutfield.velocity = Vector2.zero;
            
            homeGoalkeeper.isKinematic = true;
            homeGoalkeeper.velocity = Vector2.zero;
            homeGoalkeeper.transform.position = homeGoalkeeperStartingPosition.position;
            homeGoalkeeper.transform.localScale = new Vector3(1f, 1f, 1f);
            homeGoalkeeper.isKinematic = false;
            homeGoalkeeper.velocity = Vector2.zero;
            
            awayOutfield.isKinematic = true;
            awayOutfield.velocity = Vector2.zero;
            awayOutfield.transform.position = awayOutfieldStartingPosition.position;
            awayOutfield.transform.localScale = new Vector3(-1f, 1f, 1f);
            awayOutfield.isKinematic = false;
            awayOutfield.velocity = Vector2.zero;
            
            awayGoalkeeper.isKinematic = true;
            awayGoalkeeper.velocity = Vector2.zero;
            awayGoalkeeper.transform.position = awayGoalkeeperStartingPosition.position;
            awayGoalkeeper.transform.localScale = new Vector3(-1f, 1f, 1f);
            awayGoalkeeper.isKinematic = false;
            awayGoalkeeper.velocity = Vector2.zero;
        }
    }
}