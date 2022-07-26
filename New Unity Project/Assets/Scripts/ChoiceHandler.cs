using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChoiceHandler : MonoBehaviour
{
    Dropdown dropdown;
    public Text description;
    void Awake()
    {
        dropdown = transform.GetComponentInChildren<Dropdown>();
        dropdown.ClearOptions();
    }

    //called by the step
    public void FillDropdown(string descr)
    {
        description.text = descr;
        Cursor.lockState = CursorLockMode.None;
        dropdown.ClearOptions();
        dropdown.options.Add(new Dropdown.OptionData() { text = "Option A" });
        dropdown.options.Add(new Dropdown.OptionData() { text = "Option B" });
        dropdown.SetValueWithoutNotify(-1);
    }
    public void SolutionSelected()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ((MakeDecisionStep)QuestManager.instance.activeStep).MakeChoice(dropdown.value);
    }
}
