using UnityEngine;
using System.Collections;

public class PlayerCanvasController : MonoBehaviour
{
    public CanvasController paused;
    public CanvasController main;
    public CanvasController menu;
    public CanvasController options;
    public GameStatusController game;

    void Update()
    {
        menu.lastPaused = !game.gameOngoing;// && !optionsShown;
        main.lastPaused = game.gameOngoing;
        paused.lastPaused = game.paused;// && !optionsShown;
    }

    public void showOptions()
    {
        options.lastPaused = true;
        menu.lastPaused = false;
        paused.lastPaused = false;
        main.lastPaused = false;
    }

    public void hideOptions()
    {
        options.lastPaused = false;
        menu.lastPaused = !game.gameOngoing;
        paused.lastPaused = game.paused;
        main.lastPaused = game.gameOngoing;
    }
}
