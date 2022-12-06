using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject instructionsPanel;
        [SerializeField] private GameObject creditsPanel;

        [SerializeField] private MainMenuStadium menuStadium;
        
        public void PlayGame ()
        {
            mainMenuPanel.SetActive(false);
            menuStadium.PlayStadiumFillAnimation();
        }

        public void ShowInstructions ()
        {
            instructionsPanel.SetActive(true);
            mainMenuPanel.SetActive(false);    
        }

        public void HideInstructions ()
        {
            mainMenuPanel.SetActive(true);
            instructionsPanel.SetActive(false);
        }

        public void ShowCredits ()
        {
            creditsPanel.SetActive(true);
            mainMenuPanel.SetActive(false);    
        }

        public void HideCredits ()
        {
            mainMenuPanel.SetActive(true);
            creditsPanel.SetActive(false);    
        }

        public void ExitGame ()
        {
            Application.Quit();
        }

        public void ReturnToMain ()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
