using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitialization : MonoBehaviour
{
    private List<Character> characters = new();
    private List<CharacterInfoCard> infoCards = new();

    private void Start()
    {
        var charConf = GameRoot.Configurations.CharactersConfiguration;

        var canvasLayers = GameRoot.CanvasLayers;

        var o = new GameObject().AddComponent<RealizationOfCiclicList>();
        o.transform.SetParent(canvasLayers.Layer1);
        o.SetPrefub(ResourcesManager.CreatePrefabInstance<CharacterInfoCard, EViews>(EViews.CharacterView, canvasLayers.Layer1));
        
        foreach (ECharacters eCharacter in Enum.GetValues(typeof(ECharacters)))
        {
            var characterProps = charConf.GetConfiguration(eCharacter);

            var newCharacter = new GameObject().AddComponent<Character>();
            newCharacter.SetCharacter(characterProps);
            newCharacter.gameObject.SetActive(false);
            
            characters.Add(newCharacter);
        }

        var characterCopy = new List<Character>(characters);
        o.LoadData((card, character) =>
        {
            card.SetCharacterInfo(character);
        }, characterCopy);
        
        

        // foreach (ECharacters eCharacter in Enum.GetValues(typeof(ECharacters)))
        // {
        //     var characterProps = charConf.GetConfiguration(eCharacter);
        //     
        //     var newGameObject = new GameObject();
        //     var newCharacter = newGameObject.AddComponent<Character>();
        //     newCharacter.SetCharacter(characterProps);
        //     newCharacter.gameObject.SetActive(false);
        //     
        //     characters.Add(newCharacter);
        //
        //     var characterView = ResourcesManager.CreatePrefabInstance<CharacterInfoCard, EViews>(EViews.CharacterView, canvasLayers.Layer1);
        //     characterView.SetCharacterInfo(newCharacter);
        //     characterView.IsVisible = false;
        //     infoCards.Add(characterView);
        // }
        //
        // currentCard = infoCards[0];
        // currentCard.IsVisible = true;
    }

    private CharacterInfoCard currentCard;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            SwitchToCard(0);

        if (Input.GetKey(KeyCode.Alpha2))
            SwitchToCard(1);

        if (Input.GetKey(KeyCode.Alpha3))
            SwitchToCard(2);

        if (Input.GetKey(KeyCode.Alpha4))
            SwitchToCard(3);
    }

    private void SwitchToCard(int index)
    {
        currentCard.IsActive = false;
        currentCard = infoCards[index];
        currentCard.IsActive = true;
    }
}
