using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; private set; }

    public Quest activeQuest;
    public Step activeStep;
    List<Quest> quests = new List<Quest>();
    PlayerModel playerState;
    WorldModel worldState;

    //rewards
    // List<Reward> rewards = new List<Reward>();
    [SerializeField]
    public Text rewardText;

    [SerializeField]
    //step prefabs
    public List<GameObject> initialStepPrefabs = new List<GameObject>();
    public List<GameObject> stepPrefabs = new List<GameObject>();

    //solution dropdown
    public Dropdown dropdown;
    public GameObject solutionCanvas;

    //textbox
    public GameObject textbox;

    //rating
    public GameObject ratingCanvas;
    PlayerModel tempPlayerModel;

    //display player model
    public GameObject modelCanvas;
    public Text modelText;

    //puzzle
    public GameObject puzzleCanvas;

    //make choice
    public GameObject choiceCanvas;

    //world model
    public Text worldModelText;

    //WIP
    int i = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    //displays the text associated with each step
    public void DisplayText(string text)
    {
        textbox.SetActive(true);
        textbox.GetComponentInChildren<Text>().text = text;
    }

    public void SelectFirstQuest()
    {
        activeQuest = quests[0];
        activeQuest.startQuest();
    }

    //selects and starts next quest
    public void SelectNextQuest()
    {

        activeQuest = playerState.SelectQuest(worldState.FilterQuests(quests));
      //  activeQuest = quests[i++ % quests.Count];

        Debug.Log("starting new quest");
        activeQuest.startQuest();

    }

    void Start()
    {
        playerState = new PlayerModelExample();
        worldState = new WorldModelExample(new int[] {-1,-1,-1,-1,-1,-1 });
        ((WorldModelExample)worldState).PrintModel();
        solutionCanvas.SetActive(false);
        textbox.SetActive(false);
        ratingCanvas.SetActive(false);
        modelCanvas.SetActive(false);
        puzzleCanvas.SetActive(false);
        choiceCanvas.SetActive(false);
        InitializeQuests();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ShowModelCanvas();
        }
    }

    void InitializeQuests()
    {
        //q0
        Quest temp = new Quest(new List<SolutionInfo>() {
        new SolutionInfo("Repair wheelbarrow", new List<StepInfo>() { new StepInfo(4, new Vector3(-22, 0, -4), "You apply your magic powers to the wheelbarrow, repairing it.")}, new PlayerModelExample(new float[]{0,0,0,1,0}), new WorldModelExample(new int[]{1,0,0,0,0,0 })),
        new SolutionInfo("Kill 1 normal monster", new List<StepInfo>() { new StepInfo(3, new Vector3(-20, 0, 5), "You kill the monster.")}, new PlayerModelExample(new float[]{0,0,1,0,0}), new WorldModelExample(new int[]{1,0,0,0,0,0 })),
        new SolutionInfo("Deliver item to the mage", new List<StepInfo>() { new StepInfo(2, new Vector3(-79, 0, 3), "The villager gives you the item."),new StepInfo(1, new Vector3(28, 0, 30), "You give the mage the item.")}, new PlayerModelExample(new float[]{1,0,0,0,0}), new WorldModelExample(new List<Reward>(){new Reward("10 coins")},new int[]{1,0,0,0,4,0 })),
        new SolutionInfo("Open 1 treasure chest", new List<StepInfo>() { new StepInfo(5, new Vector3(25, 0, 40), "The chest is locked.")}, new PlayerModelExample(new float[]{0,1,0,0,0}), new WorldModelExample(new int[]{1,0,0,0,0,0 })),
        }, new StepInfo(0, new Vector3(-50, 0, -8), "The villager doesn't know you, but if you help them, they might start trusting you."), new WorldModelExample(new int[] { -1, 0, 0, 0, 0, 0 }));
        quests.Add(temp);

        GameObject steptemp = Instantiate(stepPrefabs[6], new Vector3(-79, 0, 3), Quaternion.identity);
        Solution so3 = new Solution("Give advice to the leader of village A", new List<GameObject> { steptemp }, new PlayerModelExample(new float[] { 0, 0, 0, 0, 1 }), new WorldModelExample(new int[] { 1, 0, 0, 0, 0, 0 }));
        steptemp.GetComponent<MakeDecisionStep>().Description = "The leader of village A is unsure if they should raise the price of crops, which would be good for village A, but bad for village B.";
        steptemp.GetComponent<MakeDecisionStep>().ParentSolution = so3;
        steptemp.GetComponent<MakeDecisionStep>().optionTexts = new string[] { "Keep the price low.", "Raise the price." };
        temp.AddSolution(so3);

        //q1
        quests.Add(new Quest(new List<SolutionInfo>() {
        new SolutionInfo("Collect 3 firewood", new List<StepInfo>() { new StepInfo(0, new Vector3(20, 0, -30), "You collect a pile of firewood."), new StepInfo(0, new Vector3(25, 0, -10), "You collect a pile of firewood."),new StepInfo(0, new Vector3(20, 0, 14), "You collect a pile of firewood."),new StepInfo(1, new Vector3(33, 0, -63), "You hand over the firewood.")}, new PlayerModelExample(new float[]{2,0,0,0,0}), new WorldModelExample(new List<Reward> { new Reward("boots")},new int[] { 0, 1, 0, 0, 0, 0 })),
        new SolutionInfo("Kill 3 dangerous monsters", new List<StepInfo>() { new StepInfo(8, new Vector3(20, 0, -32), "You kill the dangerous monster."),new StepInfo(8, new Vector3(25, 0, -10), "You kill the dangerous monster."),new StepInfo(8, new Vector3(25, 0, 16), "You kill the dangerous monster.")}, new PlayerModelExample(new float[]{0,0,3,0,0}), new WorldModelExample(new int[] { 0, 1, 0, 0, 0, 0 }))
        }, new StepInfo(0, new Vector3(33, 0, -63), "The villager doesn't know you, but if you help them, they might start trusting you."), new WorldModelExample(new int[] { 0, -1, 0, 0, 0, 0 })));

        //q2
        quests.Add(new Quest(new List<SolutionInfo>() {
        new SolutionInfo("Show magic powers", new List<StepInfo>() { new StepInfo(9, new Vector3(30, 0, 30), "You demonstrate your magic powers.")}, new PlayerModelExample(new float[]{0,0,0,1,0}), new WorldModelExample( new int[] { 0, 0, 0, 0, 1, 1 })),
        new SolutionInfo("Find a gift", new List<StepInfo>() { new StepInfo(5, new Vector3(45, 0, 17), "The chest is locked."),new StepInfo(1, new Vector3(28, 0, 30), "You hand over the gift.")}, new PlayerModelExample(new float[]{0,1,0,0,0}), new WorldModelExample(new int[] { 0, 0, 0, 0, 1, 0 }))
        }, new StepInfo(0, new Vector3(28, 0, 30), "The mage is unwilling to talk to you."), new WorldModelExample(new int[] { 0, 0, 0, 0, -1, -1 })));

        //q3
        Quest temp2 = new Quest(new List<SolutionInfo>() {
        new SolutionInfo("Attack village B", new List<StepInfo>() { new StepInfo(10, new Vector3(33, 0, -50), "You kill a member of village B, escalating the conflict.")}, new PlayerModelExample(new float[]{0,0,1,0,0}), new WorldModelExample( new int[] { 0, -1, -1, 1, 0, 0 }))
        }, new StepInfo(0, new Vector3(-50, 0, -8), "The villager explains to you that village B is suspected of stealing from them."), new WorldModelExample(new int[] { 1, 0, 0, 0, 0, 0 }));
        quests.Add(temp2);

        GameObject steptemp2 = Instantiate(stepPrefabs[6], new Vector3(-79, 0, 3), Quaternion.identity);
        Solution so10 = new Solution("Talk to the leader of village A", new List<GameObject> { steptemp2 }, new PlayerModelExample(new float[] { 0, 0, 0, 0, 1 }), new WorldModelExample(new int[] { 0, 0, 0, -1, 0, 0 }));
        steptemp2.GetComponent<MakeDecisionStep>().Description = "The leader of village A is outraged and considers attacking village B, you can either take the blame or try to talk the leader out of it.";
        steptemp2.GetComponent<MakeDecisionStep>().ParentSolution = so10;
        steptemp2.GetComponent<MakeDecisionStep>().optionTexts = new string[] { "The village leader is surprised, but forgives you.", "You manage to talk the village leader out of attacking village B." };
        temp2.AddSolution(so10);

        //q4
        quests.Add(new Quest(new List<SolutionInfo>() {
        new SolutionInfo("Kill 3 normal monsters", new List<StepInfo>() { new StepInfo(3, new Vector3(20, 0, -20), "You kill the monster."),new StepInfo(3, new Vector3(-15, 0, 25), "You kill the monster."),new StepInfo(3, new Vector3(0, 0, -6), "You kill the monster.") }, new PlayerModelExample(new float[]{3,0,3,0,0}), new WorldModelExample(new List<Reward> { new Reward("sword")})),
        new SolutionInfo("Open 3 trasure chests", new List<StepInfo>() { new StepInfo(5, new Vector3(0, 0, -6), "The chest is locked."),new StepInfo(5, new Vector3(20, 0, -20), "The chest is locked."),new StepInfo(5, new Vector3(-10, 0, -50), "The chest is locked."),new StepInfo(1, new Vector3(28, 0, 30), "You deliver the items.") }, new PlayerModelExample(new float[]{2,3,0,0,0}), new WorldModelExample(new List<Reward> { new Reward("20 coins")})),
        new SolutionInfo("Collect 3 herbs", new List<StepInfo>() { new StepInfo(7, new Vector3(10, 0, 0), "You collect the herbs."),new StepInfo(7, new Vector3(20, 0, -20), "You collect the herbs."),new StepInfo(7, new Vector3(-20, 0, -50), "You collect the herbs."),new StepInfo(1, new Vector3(28, 0, 30), "You deliver the items.")}, new PlayerModelExample(new float[]{2,0,0,0,0}), new WorldModelExample(new List<Reward> { new Reward("20 coins")}))
        }, new StepInfo(0, new Vector3(28, 0, 30), "The mage needs your help."), new WorldModelExample(new int[] { 0, 0, 0, 0, 1, 0 })));

        //q5
        quests.Add(new Quest(new List<SolutionInfo>() {
        new SolutionInfo("Repair 3 barrels", new List<StepInfo>() { new StepInfo(9, new Vector3(-53, 0, -7), "You repair the barrel."),new StepInfo(9, new Vector3(-55, 0, -6), "You repair the barrel."),new StepInfo(9, new Vector3(-52, 0, -4), "You repair the barrel.")}, new PlayerModelExample(new float[]{2,0,0,3,0}), new WorldModelExample(new List<Reward> { new Reward("20 coins")})),
        new SolutionInfo("Deliver gift to village B", new List<StepInfo>() { new StepInfo(2, new Vector3(-47, 0, -28), "You are given the gift to be delivered."),new StepInfo(1, new Vector3(33, 0, -63), "You hand over the gift.")}, new PlayerModelExample(new float[]{1,0,0,0,0}), new WorldModelExample(new List<Reward> { new Reward("backback")},new int[] { 0, 0, 1, -1, 0, 0 }))
        }, new StepInfo(0, new Vector3(-50, 0, -8), "The villager needs help with a number of things."), new WorldModelExample(new int[] { 1, 0, 0, 0, 0, 0 })));

        //q6
        quests.Add(new Quest(new List<SolutionInfo>() {
        new SolutionInfo("Deliver it to village A", new List<StepInfo>() { new StepInfo(1, new Vector3(-47, 0, -28), "You hand over the herbs.")}, new PlayerModelExample(new float[]{1,0,0,0,0}), new WorldModelExample(new List<Reward> { new Reward("10 coins")},new int[] { 1, 0, 0, 0, 0, 0 })),
        new SolutionInfo("Deliver it to village B", new List<StepInfo>() { new StepInfo(1, new Vector3(33, 0, -63), "You hand over the herbs.")}, new PlayerModelExample(new float[]{1,0,0,0,0}), new WorldModelExample(new List<Reward> { new Reward("10 coins") },new int[] { 0, 1, 0, 0, 0, 0 }))
        }, new StepInfo(1, new Vector3(-40, 0, 30), "You find a sack of herbs."), new WorldModelExample(new int[] { 0, 0, 0, 0, 0, 0 })));

        SelectFirstQuest();
    }

    public void UpdatePlayerModel(float weight)
    {
        playerState.UpdateModel(tempPlayerModel, weight);
        ratingCanvas.SetActive(false);
        SelectNextQuest();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateModels(PlayerModel playerImpact, WorldModel worldImpact)
    {
        ratingCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        tempPlayerModel = playerImpact;
        worldState.UpdateModel(worldImpact);
    }

    //model canvas
    public void ShowModelCanvas()
    {
        Cursor.lockState = CursorLockMode.None;
        modelCanvas.SetActive(true);
        modelText.text = playerState.GetMenuInfo();
    }

    public void CloseModelCanvas()
    {
        modelCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void IncreaseValue(int index)
    {
        ((PlayerModelExample)playerState).values[index] += 1.0f;
        modelText.text = playerState.GetMenuInfo();
    }

    public void DecreaseValue(int index)
    {
        ((PlayerModelExample)playerState).values[index] -= 1.0f;
        modelText.text = playerState.GetMenuInfo();
    }

}

//structs used for cleaner quest construction
public struct StepInfo
{
    public int index;
    public Vector3 position;
    public string description;
    public StepInfo(int index, Vector3 position, string description)
    {
        this.index = index;
        this.position = position;
        this.description = description;
    }
}

public struct SolutionInfo
{
    public List<StepInfo> stepInfos;
    public string description;
    public PlayerModel playerModel;
    public WorldModel worldModel;

    public SolutionInfo(string description, List<StepInfo> stepInfos, PlayerModel playerModel, WorldModel worldModel)
    {
        this.stepInfos = stepInfos;
        this.description = description;
        this.playerModel = playerModel;
        this.worldModel = worldModel;
    }
}
