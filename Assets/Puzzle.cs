using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class Puzzle : PuzzleController {

    public PlayMakerFSM fsm;

    [SerializeField] GameObject[] tiles;

    [SerializeField]Vector3[] startingPos;

    private bool win = false;
    private void Start()
    {
        int i=0;
        foreach (var tile in tiles)
        {
            startingPos[i] = tile.transform.localPosition;
            i++;
        }
    }

    private void Update()
    {
        if (win) { return; }
        CheckIfWin();
    }
    public override void Skip()
    {
        foreach(var tile in tiles)
        {
            tile.SetActive(false);
        }
        base.Win();
    }

    public override void ResetPuzzle()
    {
        int i = 0;
        foreach(var tile in tiles)
        {
            tile.transform.localPosition = startingPos[i];
            tile.SetActive(true);
            i++;
        }
    }

    private void CheckIfWin()
    {
        int i = 0;
        foreach(var tile in tiles)
        { 
            if (tile.transform.gameObject.activeInHierarchy)
            {
                return;
            }
            i++;
        }

        base.Win();
        win = true;
        fsm.SendEvent("won");
    }
}
