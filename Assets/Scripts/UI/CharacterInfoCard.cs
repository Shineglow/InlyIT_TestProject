using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoCard : MonoBehaviour, ICharacterInfoCard, ITransparent
{
    [SerializeField] 
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI additionalInfoText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    private float transperencyOld = 0;
    [SerializeField, Range(0,1)]
    private float transparency = 1;

    public float Transparency
    {
        get => transparency;
        set => transparency = MathF.Max(MathF.Min(0, value), 1);
    }

    private List<Graphic> graphics = new();

    private void OnValidate()
    {
        if (Math.Abs(transparency - transperencyOld) > 0.001)
        {
            transperencyOld = transparency;
            graphics.Clear();
            List<Transform> childsWithChilds = new() { transform };
            
            for (var i = 0; i < childsWithChilds.Count; i++)
            {
                var child = childsWithChilds[i];
                if (child.TryGetComponent<Graphic>(out var component))
                {
                    var oldColor = component.color;
                    oldColor.a = transparency;
                    component.color = oldColor;
                }
                for(var y = 0; y < child.childCount; y++)
                    childsWithChilds.Add(child.GetChild(y));
            }
        }
    }

    public void SetCharacterInfo(ICharacter character, string additionalInfo = null)
    {
        icon.sprite = character.Icon;
        nameText.text = character.ObjectName;
        additionalInfoText.text = additionalInfo ?? string.Empty;
        descriptionText.text = character.Description;
    }

    public bool IsActive
    {
        get => gameObject.activeSelf; 
        set => gameObject.SetActive(value);
    }
}

public interface ICharacterInfoCard : IView
{
    void SetCharacterInfo(ICharacter character, string additionalInfo = null);
}

public interface ITransparent
{
    float Transparency { get; set; }
}
