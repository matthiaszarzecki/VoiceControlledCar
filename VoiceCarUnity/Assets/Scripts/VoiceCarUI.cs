using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceCarUI : MonoBehaviour {

    public GameObject UIMenu;
    public GameObject UIInGame;
    public GameObject UILevelComplete;
    public Text distanceText;
    public Game game;

	void Update () {
        if (game.currentState == Game.gameStates.inGame) {
            distanceText.text = game.GetDistance().ToString("F1") + "m";
        }
	}

    public void CycleBodies() {
        game.CycleBodies();
    }

    public void CycleEngines() {
        game.CycleEngines();
    }

    public void CycleTractions() {
        game.CycleTractions();
    }

    public void BeginGame() {
        game.BeginGame();
    }

    public void RestartGame() {
        game.RestartGame();
    }

    public void SetMenu(Game.gameStates newState) {
        UIMenu.SetActive(false);
        UIInGame.SetActive(false);
        UILevelComplete.SetActive(false);

        if(newState == Game.gameStates.menu)
            UIMenu.SetActive(true);
        else if(newState == Game.gameStates.inGame)
            UIInGame.SetActive(true);
        else if(newState == Game.gameStates.levelComplete)
            UILevelComplete.SetActive(true);
    }
}
