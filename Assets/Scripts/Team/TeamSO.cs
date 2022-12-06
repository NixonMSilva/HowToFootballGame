using UnityEngine;

namespace Team
{
    [CreateAssetMenu(menuName = "SO/Team", fileName = "New Team")]
    public class TeamSO : ScriptableObject
    {
        public string teamName;
        public Color color1, color2;
    }
}