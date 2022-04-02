using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager instance;
    public MenuField menuField;
    [SerializeField] private List<Field> starterFields = new List<Field>();
    [SerializeField] private List<Field> easyFields = new List<Field>();
    [SerializeField] private int easyFieldsThreshold = 0;
    [SerializeField] private List<Field> normalFields = new List<Field>();
    [SerializeField] private int normalFieldsThreshold = 0;
    [SerializeField] private List<Field> hardFields = new List<Field>();
    [SerializeField] private int hardFieldsThreshold = 0;
    private List<Field> activeFields = new List<Field>();
    private bool easyFieldsAdded = false;
    private bool normalFieldsAdded = false;
    private bool hardFieldsAdded = false;
    private int currentField = 0;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);    
    }

    private void OnEnable() 
    {
        AddStarterFields();    
    }

    public void ClearFields()
    {
        activeFields.Clear();
        easyFieldsAdded = false;
        normalFieldsAdded = false;
        hardFieldsAdded = false;
        AddStarterFields();
    }

    private void Start() 
    {
        ScoreManager.instance.OnAddScore += CheckCurrentScore;
    }

    private void OnDisable() 
    {
        ScoreManager.instance.OnAddScore -= CheckCurrentScore;
    }

    public Field ChooseNextField()
    {
        var tempFieldNumber = Random.Range(0, activeFields.Count);
        if(tempFieldNumber == currentField)
        {
            return ChooseNextField();
        }
        else
        {
            currentField = tempFieldNumber;
            return activeFields[tempFieldNumber];
        }
    }

    public void CheckCurrentScore()
    {
        if(!easyFieldsAdded)
        {
            if(ScoreManager.instance.currentScore >= easyFieldsThreshold)
            {
                AddEasyFields();
                easyFieldsAdded = true;
            }
        }
        else
        {
            if(!normalFieldsAdded)
            {
                if(ScoreManager.instance.currentScore >= normalFieldsThreshold)
                {
                    AddNormalFields();
                    normalFieldsAdded = true;
                }
            }
            else
            {
                if(!hardFieldsAdded)
                {
                    if(ScoreManager.instance.currentScore >= hardFieldsThreshold)
                    {
                        AddHardFields();
                        hardFieldsAdded = true;
                    }
                }
            }
        }
    }
    private void AddStarterFields()
    {
        activeFields.AddRange(starterFields);
    }

    private void AddEasyFields()
    {
        activeFields.AddRange(easyFields);
    }

    private void AddNormalFields()
    {
        activeFields.AddRange(normalFields);
    }

    private void AddHardFields()
    {
        activeFields.AddRange(hardFields);
    }

    public void HideMenuField()
    {
        menuField.Hide();
    }

    public void ShowMenuField()
    {
        menuField.RollOut();
    }
}
