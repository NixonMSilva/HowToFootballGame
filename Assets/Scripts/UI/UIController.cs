using System;
using Core;
using UnityEngine;
using TMPro;
using System.Collections;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIStringsSO strings;
        
        [SerializeField] private TextMeshProUGUI homeScore;
        [SerializeField] private TextMeshProUGUI awayScore;

        [SerializeField] private CanvasGroup utilityPanel;
        [SerializeField] private TextMeshProUGUI utilityPanelText;

        [SerializeField] private CanvasGroup returnToMainPanel;

        private void OnEnable ()
        {
            GameManager.Instance.OnScore += ShowGoalMessage;
            GameManager.Instance.OnScoreNoCallback += ChangeScoreValue;
            GameManager.Instance.OnGameReady += ShowReadyMessage;
        }

        private void OnDisable ()
        {
            GameManager.Instance.OnScore -= ShowGoalMessage;
            GameManager.Instance.OnScoreNoCallback -= ChangeScoreValue;
            GameManager.Instance.OnGameReady -= ShowReadyMessage;
        }

        private void ChangeScoreValue (MatchSide scoringSide)
        {
            switch (scoringSide)
            {
                case MatchSide.Home:
                    homeScore.text = GameManager.Instance.ScoreHome.ToString();
                    break;
                case MatchSide.Away:
                    awayScore.text = GameManager.Instance.ScoreAway.ToString();
                    break;
                default:
                    break;
            }
        }

        private void ShowGoalMessage (MatchSide scoringSide, Action callback = null)
        {
            var goalscorerName = scoringSide switch
            {
                MatchSide.Home => GameManager.Instance.HomeTeam.teamName,
                MatchSide.Away => GameManager.Instance.AwayTeam.teamName,
                _ => "<EMPTY>"
            };

            if (GameManager.Instance.IsMatchOver())
                ShowUtilityMessage(strings.GoalPreffix + " " + goalscorerName, () => ShowVictoryMessage(scoringSide));
            else
                ShowUtilityMessage(strings.GoalPreffix + " " + goalscorerName, () => ShowMessUpMessage(callback));
        }

        private void ShowMessUpMessage (Action callback = null)
        {
            ShowUtilityMessage(strings.MessUpPhaseStart, callback);
        }
        
        private void ShowReadyMessage ()
        {
            ShowUtilityMessage (strings.MatchReady);
        }

        private void ShowVictoryMessage (MatchSide victorSide)
        {
            var victoryString = victorSide switch
            {
                MatchSide.Home => GameManager.Instance.HomeTeam.teamName,
                MatchSide.Away => GameManager.Instance.AwayTeam.teamName,
                _ => "<EMPTY>"
            };
            victoryString += " " + strings.VictorySuffix;
            ShowUtilityMessage(victoryString, ShowReturnToMainPanel);
        }

        private void ShowUtilityMessage (string message, Action callback = null)
        {
            utilityPanelText.text = message;
            ShowPanel(utilityPanel, 0.2f, () =>
            {
                StartCoroutine(ErasePanel(utilityPanel, 2.3f, 0.2f, callback));
            });
        }

        private void ShowReturnToMainPanel ()
        {
            returnToMainPanel.alpha = 1f;
        }
        
        private static IEnumerator ElementFade (CanvasGroup element, float duration, float start, float end, Action callback = null)
        {
            var startTime = Time.time;
            var endTime = Time.time + duration;

            while (Time.time <= endTime)
            {
                var elapsedTime = Time.time - startTime;
                var percentage = 1f / (duration / elapsedTime);
                if (start > end)
                {
                    element.alpha = start - percentage;
                }
                else
                {
                    element.alpha = start + percentage;
                }
                yield return new WaitForEndOfFrame();
            }

            element.alpha = end;
            
            callback?.Invoke();
        }

        private void ShowPanel (CanvasGroup panel, float fadeInTime, Action callback = null)
        {
            StartCoroutine(ElementFade(panel, fadeInTime, 0f, 1f, callback));
        }

        private IEnumerator ErasePanel (CanvasGroup panel, float waitTime, float fadeOutTime, Action callback = null)
        {
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(ElementFade(panel, fadeOutTime, 1f, 0f, callback));
        }
    }
}