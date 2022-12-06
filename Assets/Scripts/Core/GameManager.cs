using System;
using Droppables;
using UnityEngine;
using Team;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private TeamSO homeTeam, awayTeam;

        [SerializeField] private int scoreLimit = 5;

        public Action<MatchSide, Action> OnScore;

        public Action<MatchSide> OnScoreNoCallback;

        public Action OnClawsStarted;

        public Action OnClawReleased;

        public Action OnGameReady;

        public Action OnCrossbar;

        public Action OnKick;

        public Action<MatchSide> OnVictory;

        public Action OnMatchEnd;

        public Action<PlayerType> OnPlayerJump;

        public Action<DroppableType> OnDroppableDrop;
        
        public bool gameIsOn = true;

        public int ScoreHome { get; private set; } = 0;

        public int ScoreAway { get; private set; } = 0;
        
        public TeamSO HomeTeam => homeTeam;

        public TeamSO AwayTeam => awayTeam;
        
        private void Awake ()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject, 1f);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable ()
        {
            OnScoreNoCallback += AddScore;
        }

        private void OnDisable ()
        {
            OnScoreNoCallback -= AddScore;
        }

        private void Start()
        {
            OnGameReady?.Invoke();
        }

        private void AddScore (MatchSide side)
        {
            gameIsOn = false;
            
            switch (side)
            {
                case MatchSide.Home:
                    ScoreHome++;
                    if (ScoreHome >= scoreLimit)
                        DeclareVictor(MatchSide.Home);
                    break;
                case MatchSide.Away:
                    ScoreAway++;
                    if (ScoreAway >= scoreLimit)
                        DeclareVictor(MatchSide.Away);
                    break;
                default:
                    break;
            }
        }

        private void DeclareVictor (MatchSide side)
        {
            OnVictory?.Invoke(side);
        }

        public bool IsMatchOver () => (ScoreHome == scoreLimit) || (ScoreAway == scoreLimit);
    }
}