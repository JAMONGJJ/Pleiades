using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScript : MonoBehaviour
{
    public GameObject MainQuestPanel;
    public GameObject SubQuestPanel;

    public GameObject MainQuestContent;
    public GameObject SubQuestContent;
    public GameObject QuestPrefab;
    List<GameObject> quests;
    RectTransform questRT;

    private void OnEnable()
    {
        quests = new List<GameObject>();
    }

    private void OnDisable()
    {
        foreach (var o in quests)
            Destroy(o.gameObject);
        Resources.UnloadUnusedAssets();
    }

    public void MainPanelOpen()
    {
        MainQuestPanel.SetActive(true);
        SubQuestPanel.SetActive(false);
    }
    public void SubPanelOpen()
    {
        SubQuestPanel.SetActive(true);
        MainQuestPanel.SetActive(false);
    }

    void ViewMain()
    {

    }

    void ViewSub()
    {
        questRT = SubQuestContent.GetComponent<RectTransform>();

    }
}
