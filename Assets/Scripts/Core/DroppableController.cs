using Droppables;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class DroppableController : MonoBehaviour
    {
        [SerializeField] private List<Droppable> droppablesInScene = new List<Droppable>();

        private void OnEnable()
        {
            GameManager.Instance.OnGameReady += GetAllActiveDroppables;
            GameManager.Instance.OnClawsStarted += WipeAllDroppables;
        }

        private void OnDisable()
        {            
            GameManager.Instance.OnGameReady -= GetAllActiveDroppables;
            GameManager.Instance.OnClawsStarted -= WipeAllDroppables;
        }

        private void GetAllActiveDroppables ()
        {
            var objects = GameObject.FindObjectsOfType(typeof(Droppable));
            foreach (var o in objects)
            {
                var obj = (Droppable)o;
                droppablesInScene.Add(obj);
            }
        }

        private void WipeAllDroppables ()
        {
            foreach (var droppable in droppablesInScene.ToList().Where(droppable => droppable != null))
            {
                droppablesInScene.Remove(droppable);
                Destroy(droppable.gameObject);
            }
        }
    }
}