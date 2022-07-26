using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InitialStep : MonoBehaviour
{
    Collider trigger;
    GameObject marker;
    public abstract string Description { get; set; }

    public Quest ParentQuest { get; set; }

    private bool triggerEntered = false;

    private void OnTriggerEnter()
    {
        triggerEntered = true;
        LogStep();
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

    //accomplish step, notify quest
    public abstract void StartStep();

    public void LogStep()
    {
        QuestManager.instance.DisplayText(Description);
    }
}
