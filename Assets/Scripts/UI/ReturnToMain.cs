using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ReturnToMain : MonoBehaviour
    {
        public void Return ()
        {
            if (!GameManager.Instance.IsMatchOver())
                return;
            
            SceneManager.LoadScene("MenuScene");
        }
    }
}