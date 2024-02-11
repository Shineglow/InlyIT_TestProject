using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurations
{
    private BaseConfiguration<ICharacterProperties, ECharacters> _charactersConfiguration;
    public BaseConfiguration<ICharacterProperties, ECharacters> CharactersConfiguration => 
        _charactersConfiguration ??= new CharactersConfiguration();

    public void FreeResources()
    {
        
    }
}



