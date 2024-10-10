using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    public SkillTree skillTree;
    public GameObject skillNodePrfabs;
    public RectTransform skillTreePanel;
    public float NodeSpacing = 100f;
    public Text skillPointText;
    public int totalSkillPoint = 10;

    private Dictionary<string, Button> skillButtons = new Dictionary<string, Button>();


    private void Start()
    {
        InitalizeSkillTree();
        CreateSkillTreeUI();
        UpdateSkillPointsUI();
    }

    private void InitalizeSkillTree()
    {
        skillTree = new SkillTree();

        skillTree.AddNode(new SkillNode("Fireball1", "Fireball1",
            new Skill<ISkillTarget,DamageEffect>("Fireball1", new DamageEffect(20)),
            new Vector2(0,0),"Fireball",1));

        skillTree.AddNode(new SkillNode("Fireball2", "Fireball2",
            new Skill<ISkillTarget, DamageEffect>("Fireball2", new DamageEffect(30)),
            new Vector2(1, 0), "Fireball", 2, new List<string> { "Fireball1"}));

        skillTree.AddNode(new SkillNode("Fireball3", "Fireball3",
            new Skill<ISkillTarget, DamageEffect>("Fireball3", new DamageEffect(40)),
            new Vector2(2, 0), "Fireball", 3, new List<string> { "Fireball2" }));

        skillTree.AddNode(new SkillNode("Fireball4", "Fireball4",
            new Skill<ISkillTarget, DamageEffect>("Fireball4", new DamageEffect(40)),
            new Vector2(3, 0), "Fireball", 4, new List<string> { "Fireball3" }));

        skillTree.AddNode(new SkillNode("Fireball5", "Fireball5",
            new Skill<ISkillTarget, DamageEffect>("Fireball5", new DamageEffect(40)),
            new Vector2(4, 0), "Fireball", 5, new List<string> { "Fireball4" }));

        skillTree.AddNode(new SkillNode("Fireball6", "Fireball6",
            new Skill<ISkillTarget, DamageEffect>("Fireball6", new DamageEffect(40)),
            new Vector2(5, 0), "Fireball", 6, new List<string> { "Fireball5" }));




    }

    private void CreateSkillTreeUI()
    {
        foreach(var node in skillTree.Nodes)
        {
            CreateSkillNodeUI(node);
        }
    }

    private void CreateSkillNodeUI(SkillNode node)
    {
        GameObject nodeObj = Instantiate(skillNodePrfabs, skillTreePanel);
        RectTransform rectTransform = nodeObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = node.Position * NodeSpacing;

        Button button = nodeObj.GetComponent<Button>();
        Text text = nodeObj.GetComponentInChildren<Text>();
        text.text = node.Name;

        button.onClick.AddListener(() => OnSkillNodeClicked(node.Id));
        skillButtons[node.Id] = button;
        UpdateNodeUI(node);
    }

    private void OnSkillNodeClicked(string skillId)
    {
        SkillNode node = skillTree.GetNode(skillId);

        if (node == null) return;

        if(node.isUnlocked)
        {
            if(skillTree.LockSkill(skillId))
            {
                totalSkillPoint++;
                UpdateSkillPointsUI();
                UpdateNodeUI(node);
                UpdateConnectedSkills(skillId);
            }
            else
            {
                Debug.Log("관련 연계 스킬이 있어서 해제가 안됩니다.");
            }
        }
        else if(totalSkillPoint > 0 && CanUnlockSkill(node))
        {
            if(skillTree.UnlockSkill(skillId))
            {
                totalSkillPoint--;
                UpdateSkillPointsUI();
                UpdateNodeUI(node);
                UpdateConnectedSkills(skillId);
            }
        }


    }

    private void UpdateNodeUI(SkillNode node)
    {
        if(skillButtons.TryGetValue(node.Id, out Button button))        // 동작이 일어났을 때 UI 업데이트
        {
            bool canUnlock = !node.isUnlocked && CanUnlockSkill(node);
            button.interactable = (canUnlock && totalSkillPoint > 0) || node.isUnlocked;
            button.GetComponent<Image>().color = node.isUnlocked ? Color.green : (canUnlock ? Color.yellow : Color.red);
        }
    }

    private bool CanUnlockSkill(SkillNode node)             // Lock 해제 관련 함수
    {
        foreach(var requiredSkillId in node.RequiredSkillds)
        {
            if(!skillTree.IsSkillUnlock(requiredSkillId))
            {
                return false;
            }
        }
        return true;
    }


    private void UpdateSkillPointsUI()      
    {
        skillPointText.text = $"Skill Points : {totalSkillPoint}";
    }

    private void UpdateConnectedSkills(string skillId)
    {
        foreach(var node in skillTree.Nodes)
        {
            if(node.RequiredSkillds.Contains(skillId))
            {
                UpdateNodeUI(node);
            }
        }
    }

}
