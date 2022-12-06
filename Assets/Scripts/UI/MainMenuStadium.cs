using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuStadium : MonoBehaviour
    {
        private Animator _animator;

        private void Awake ()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayStadiumFillAnimation ()
        {
            _animator.Play("StadiumMenuFill");
        }

        public void StartGame ()
        {
            SceneManager.LoadScene("MatchScene");
        }
    }
}