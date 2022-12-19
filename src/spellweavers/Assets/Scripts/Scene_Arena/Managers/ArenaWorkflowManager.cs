using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ArenaStates
{
    Ingame,
    PauseScreen,
    FeatsUpgradeScreen,
    ReturningToStartScreen,
    QuittingGame
}
public class ArenaWorkflowManager : MonoBehaviour
{

    protected static ArenaWorkflowManager manager;
    public static ArenaWorkflowManager Manager { get => manager; }

    protected ArenaStates arenaState;
    public ArenaStates ArenaState { get => arenaState; }

    protected ArenaUIController arenaUIController;

    [SerializeField] protected ScenesListSO scenesList;

    protected void Awake()
    {
        if (manager != this && manager != null)
        {
            Destroy(this);
        }
        else
        {
            manager = this;
        }

        arenaUIController = FindObjectOfType<ArenaUIController>();
    }

    protected void Start()
    {
        SwitchState(ArenaStates.Ingame);
        ArenaManager.Manager.StartGame();
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchState(ArenaStates.PauseScreen);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchState(ArenaStates.Ingame);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchState(ArenaStates.FeatsUpgradeScreen);
        }
    }

    public void SwitchState(ArenaStates state)
    {
        arenaState = state;
        switch (arenaState)
        {
            case ArenaStates.Ingame:
                arenaUIController.SwitchUI();
                PauseDisable();
                break;

            case ArenaStates.PauseScreen:
            case ArenaStates.FeatsUpgradeScreen:
                arenaUIController.SwitchUI();
                PauseEnable();
                break;
            case ArenaStates.ReturningToStartScreen:
                SceneManager.LoadScene(scenesList.GetSceneNameByKey("StartScreenScene"));
                break;
            case ArenaStates.QuittingGame:
                Application.Quit();
                break;                
        }
    }

    protected void PauseEnable()
    {
        Time.timeScale = 0.0f;
    }

    protected void PauseDisable()
    {
        Time.timeScale = 1.0f;
    }

    

}
