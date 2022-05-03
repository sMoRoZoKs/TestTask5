using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private BoardController boardController;
        [SerializeField] private bool isStrongAI;
        [SerializeField] private GameObject winMenu;
        [SerializeField] private GameObject deffMenu;
        [SerializeField] private GameObject endGameMenu;
        private BaseAIModul _aiModul;

        private void Start()
        {
            if (!isStrongAI) _aiModul = new LowAIModul(boardController);
            else _aiModul = new StrongAIModul(boardController);
            winMenu.SetActive(false);
            deffMenu.SetActive(false);
            endGameMenu.SetActive(false);
            boardController.WinEvents.AddEvent(() =>
            {
                winMenu.SetActive(true);
            });
            boardController.DeffEvents.AddEvent(() =>
            {
                deffMenu.SetActive(true);
            });
            boardController.EndGameEvents.AddEvent(() =>
            {
                endGameMenu.SetActive(true);
            });
        }
    }
}