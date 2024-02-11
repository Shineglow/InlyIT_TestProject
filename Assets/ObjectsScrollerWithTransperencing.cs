using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectsScrollerWithTransperencing<T, E> : MonoBehaviour where T : MonoBehaviour, ITransparent
{
    [SerializeField] 
    private T prefub;
    
    private List<T> transperentObjects;
    private List<E> data;
    private Action<T, E> setDataFunction;
    

    private const int ObjectsCount = 6;
    

    private int _currentIndexData;
    private int CurrentIndexData
    {
        get => _currentIndexData;
        set
        {
            _currentIndexData = value % data.Count;
            if (value < 0)
                _currentIndexData += data.Count;
        }
    }
    
    private int _currentIndexObjects;

    private int CurrentIndexObjects
    {
        get => _currentIndexObjects;
        set
        {
            _currentIndexObjects = value % data.Count;
            if (value < 0)
                _currentIndexObjects += data.Count;
        }
    }
    
    private const float deltaPercent = 0.1f;
    private Vector2 defaultSize;
    private Vector3 horizontalDeltaPositive = Vector3.zero;

    private List<Vector3> destinations = new();

    private void Awake()
    {
        for(var i = 0; i < ObjectsCount;i++)
            destinations.Add(default);
    }


    private IEnumerable PlaySlideAnimation(bool toRight)
    {
        var curSelected = transperentObjects[CurrentIndexObjects];
        var destination = curSelected.transform.position;

        ShiftObjectsIndexes(toRight);
        while (curSelected.transform.position != destination)
        {
            var zeroTransform = transperentObjects[0].transform as RectTransform;
            zeroTransform.position += horizontalDeltaPositive;
            zeroTransform.localScale += new Vector3(deltaPercent,deltaPercent,deltaPercent);

            yield return null;
        }
    }

    private void ShiftObjectsIndexes(bool toRight)
    {
        if (toRight)
        {
            var shifted =  transperentObjects[^1];
            for (var i = ObjectsCount - 1; i > 0; i--)
                transperentObjects[i] = transperentObjects[i - 1];
            transperentObjects[0] = shifted;
        }
        else
        {
            var shifted =  transperentObjects[0];
            for (var i = 0; i < ObjectsCount - 1; i++)
                transperentObjects[i] = transperentObjects[i+1];
            transperentObjects[^1] = shifted;
        }
    }

    public void SetPrefub([NotNull] T prefub)
    {
        this.prefub = prefub;

        defaultSize = prefub.transform.GetComponent<RectTransform>().rect.size;
        horizontalDeltaPositive.x = defaultSize.x * deltaPercent;
        {
            if(ObjectsCount % 2 == 0)
            {
                destinations[^1] = new(0,0,-1);
                destinations[destinations.Count/2-1] = new(0,0,0);

                for (var i = 0; i < destinations.Count / 2 - 2; i++)
                {
                    
                }
            }
        }
        
        if(transperentObjects is {Count: > 0})
            for (var i = 0; i < ObjectsCount; i++)
                Destroy(transperentObjects[i]);
        
        for (var i = 0; i < ObjectsCount; i++)
            transperentObjects[i] = Instantiate(prefub).GetComponent<T>();

        if (data != null)
            LoadFromCurrentAlignedIndex();
    }

    public void LoadData([NotNull] Action<T,E> setDataFunction, [NotNull] List<E> data)
    {
        this.setDataFunction = setDataFunction;
        this.data = data;

        LoadFromCurrentAlignedIndex();
    }

    private void LoadFromCurrentAlignedIndex()
    {
        CurrentIndexData = CurrentIndexData;
        var objCount = ObjectsCount;
        
        while(objCount != 0)
            for (var i = CurrentIndexData; i < ObjectsCount; i++, objCount--)
                setDataFunction(transperentObjects[i], data[i]);
    }

    public void SwitchLeft()
    {
        CurrentIndexData--;
        LoadFromCurrentAlignedIndex();
    }

    public void SwitchRight()
    {
        CurrentIndexData++;
        LoadFromCurrentAlignedIndex();
    }

    public void SelectByIndex(int index)
    {
        CurrentIndexData = index;
        LoadFromCurrentAlignedIndex();
    }
}
