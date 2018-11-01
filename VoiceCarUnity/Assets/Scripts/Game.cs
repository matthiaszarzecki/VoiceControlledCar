using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public gameStates currentState = gameStates.menu;
    public enum gameStates { menu, inGame, levelComplete, }

    public Vehicle vehicle;
    public MicrophoneReader microphone;
    public VoiceCarUI ui;
    public BackgroundObjects backgroundObjects;

    void Start() {
        SetState(gameStates.menu);
        backgroundObjects.PlaceAllObjects();
    }

	void Update () {
        if (currentState == gameStates.inGame) {
            vehicle.AddSpeed(microphone.microphoneVolume);

            if (vehicle.transform.position.x >= 0) {
                SetState(gameStates.levelComplete);
            }
        } else if (currentState == gameStates.levelComplete) {
            StopVehicle();
        } else if (currentState == gameStates.menu) {
            StopVehicle();
        }
	}

    public void SetState(gameStates newState) {
        currentState = newState;
        ui.SetMenu(currentState);
    }

    public float GetDistance() {
        return -vehicle.transform.position.x;
    }

    public void BeginGame() {
        SetState(gameStates.inGame);
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }
        
    public void CycleBodies() {
        vehicle.CycleBodies();
    }

    public void CycleEngines() {
        vehicle.CycleEngines();
    }

    public void CycleTractions() {
        vehicle.CycleTractions();
    }

    private void StopVehicle() {
        vehicle.StopVehicle();
    }

}
