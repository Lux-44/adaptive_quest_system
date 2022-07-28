using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LogicalPuzzleHandler : MonoBehaviour
{
    List<Puzzle> puzzles = new List<Puzzle>();
    public Toggle aT;
    public Toggle bT;
    public Toggle cT;
    Puzzle currentPuzzle;
    public Text puzzleText;
    int index = 0;


    private void Awake()
    {
        puzzles.Add(new Puzzle("(a && b) || c", (a, b, c) => (a && b) || c));
        puzzles.Add(new Puzzle("(a && c) || (b && c)", (a, b, c) => (a && c) || (b && c)));
        puzzles.Add(new Puzzle("(a || b) && (c && b)", (a, b, c) => (a || b) && (c && b)));
        puzzles.Add(new Puzzle("a || b || c", (a, b, c) => a || b || c));
        puzzles.Add(new Puzzle("(a || b) && c", (a, b, c) => (a || b) && c));
        puzzles.Add(new Puzzle("(a || b) && (c || b)", (a, b, c) => (a || b) && (c || b)));
        puzzles.Add(new Puzzle("(a || b) && (c || (b && a))", (a, b, c) => (a || b) && (c || (b && a))));
    }

    public void FillPuzzle()
    {
        Cursor.lockState = CursorLockMode.None;

        currentPuzzle = puzzles[index++ % puzzles.Count];
        puzzleText.text = currentPuzzle.description;
    }

    public void CheckInterpretation()
    {
        Cursor.lockState = CursorLockMode.Locked;
        bool res = currentPuzzle.eval(aT.isOn, bT.isOn, cT.isOn);
        QuestManager.instance.puzzleCanvas.SetActive(false);
        if (res)
        {
            ((SolveProblemStep)QuestManager.instance.activeStep).Correct();
        }
        else
        {
            ((SolveProblemStep)QuestManager.instance.activeStep).Incorrect();

        }
    }
}
