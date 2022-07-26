using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Step : MonoBehaviour
{
    Collider trigger;
    GameObject marker;
    public abstract string Description { get; set; }
    public Solution ParentSolution { get; set; }

    private bool triggerEntered = false;

    private void OnTriggerEnter()
    {
        triggerEntered = true;
        QuestManager.instance.activeStep = this;
    }

    private void OnTriggerExit()
    {
        triggerEntered = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggerEntered)
        {
            StartStep();
        }
    }

    public void Awake()
    {
        gameObject.SetActive(false);
    }

    //accomplish step, notify solution
    public abstract void StartStep();

    public void LogStep()
    {
        QuestManager.instance.DisplayText(Description);

    }
}
