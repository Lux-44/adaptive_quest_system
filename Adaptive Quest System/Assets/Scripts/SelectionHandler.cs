using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionHandler : MonoBehaviour
{
    Dropdown dropdown;

    void Awake()
    {
        dropdown = transform.GetComponent<Dropdown>();
        dropdown.ClearOptions();
    }

    public void FillDropdown(List<Solution> solutions)
    {
        Cursor.lockState = CursorLockMode.None;
        dropdown.ClearOptions();
        foreach (var sol in solutions)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = sol.Description});
        }
        dropdown.SetValueWithoutNotify(-1);
    }
    public void SolutionSelected()
    {
        QuestManager.instance.activeQuest.StartSolution(dropdown.value);
        QuestManager.instance.solutionCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
