using System;
using UnityEngine;
using Audio;
using Droppables;
using Random = UnityEngine.Random;

namespace Core
{
    public class AudioEvents : MonoBehaviour
    {
        [SerializeField] private Sound[] crossbarSounds;
        [SerializeField] private Sound[] kickSounds;
        [SerializeField] private Sound[] goalSounds;
        
        [SerializeField] private Sound[] startWhistleSounds;
        [SerializeField] private Sound[] goalWhistleSounds;
        [SerializeField] private Sound[] endingWhistleSounds;
        
        [SerializeField] private Sound[] crowdGoalSounds;

        [SerializeField] private Sound[] playerJumpSounds;

        [SerializeField] private Sound[] slimeDropSounds;
        [SerializeField] private Sound[] obstacleDropSounds;

        [SerializeField] private AudioSource clawLoopSound;
        
        private void OnEnable ()
        {
            GameManager.Instance.OnCrossbar += PlayCrossbarSound;
            GameManager.Instance.OnPlayerJump += PlayJumpSound;
            GameManager.Instance.OnKick += PlayKickSound;
            
            GameManager.Instance.OnGameReady += PlayStartWhistleSound;
            GameManager.Instance.OnScoreNoCallback += PlayGoalWhistleSound;
            GameManager.Instance.OnMatchEnd += PlayEndingWhistleSound;
            
            GameManager.Instance.OnScoreNoCallback += PlayCrowdGoalSound;

            GameManager.Instance.OnDroppableDrop += PlayDroppableSound;

            GameManager.Instance.OnClawsStarted += StartClawSound;
            GameManager.Instance.OnGameReady += EndClawSound;
        }

        private void OnDisable ()
        {
            GameManager.Instance.OnCrossbar -= PlayCrossbarSound;
            GameManager.Instance.OnPlayerJump -= PlayJumpSound;
            GameManager.Instance.OnKick -= PlayKickSound;
            
            GameManager.Instance.OnGameReady -= PlayStartWhistleSound;
            GameManager.Instance.OnScoreNoCallback -= PlayGoalWhistleSound;
            GameManager.Instance.OnMatchEnd -= PlayEndingWhistleSound;
            
            GameManager.Instance.OnScoreNoCallback -= PlayCrowdGoalSound;
            
            GameManager.Instance.OnDroppableDrop -= PlayDroppableSound;
            
            GameManager.Instance.OnClawsStarted -= StartClawSound;
            GameManager.Instance.OnGameReady -= EndClawSound;
        }

        private void PlayCrossbarSound ()
        {
            if (crossbarSounds.Length == 0)
                return;
            
            AudioManager.Instance.PlayRandomSound(crossbarSounds);
        }
        
        private void PlayJumpSound (PlayerType playerType)
        {
            if (playerJumpSounds.Length == 0)
                return;
            
            AudioManager.Instance.PlayRandomSound(playerJumpSounds);
        }

        private void PlayKickSound ()
        {
            if (kickSounds.Length == 0)
                return;
            
            AudioManager.Instance.PlayRandomSound(kickSounds);
        }

        private void PlayStartWhistleSound ()
        {
            if (startWhistleSounds.Length == 0)
                return;
            
            AudioManager.Instance.PlayRandomSound(startWhistleSounds);
        }
        
        private void PlayGoalWhistleSound (MatchSide side)
        {
            if (goalWhistleSounds.Length == 0)
                return;
            
            AudioManager.Instance.PlayRandomSound(goalWhistleSounds);
        }

        private void PlayEndingWhistleSound ()
        {
            if (endingWhistleSounds.Length == 0)
                return;
            
            AudioManager.Instance.PlayRandomSound(endingWhistleSounds);
        }

        private void PlayCrowdGoalSound (MatchSide side)
        {
            if (crowdGoalSounds.Length == 0)
                return;
            
            AudioManager.Instance.PlayRandomSound(crowdGoalSounds);
        }
        
        private void PlayDroppableSound (DroppableType type)
        {
            switch (type)
            {
                case DroppableType.slime:
                    AudioManager.Instance.PlayRandomSound(slimeDropSounds);
                    break;
                default:
                    AudioManager.Instance.PlayRandomSound(obstacleDropSounds);
                    break;
            }
        }

        private void StartClawSound () => clawLoopSound.enabled = true;
        
        private void EndClawSound () => clawLoopSound.enabled = false;
        
    }
}