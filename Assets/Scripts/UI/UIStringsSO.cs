using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "SO/UI String Collection", fileName = "New UI String Collection")]
    public class UIStringsSO : ScriptableObject
    {
        public string GoalPreffix = "Goal for ";
        public string MessUpPhaseStart = "Mess up with the field!";
        public string MatchReady = "Ready!";
        public string VictorySuffix = "wins!";
    }
}