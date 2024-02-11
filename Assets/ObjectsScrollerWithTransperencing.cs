using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectsScrollerWithTransperencing<T, E> : MonoBehaviour where T : MonoBehaviour, ITransparent
{
    private RectTransform rectTransform;
    
    [SerializeField] 
    private T prefub;
    
    private List<T> transperentObjects = new();
    private List<E> data;
    private Action<T, E> setDataFunction;

    [SerializeField, Min(1.2f)]
    private float width = 1.2f;
    

    private const int ObjectsCount = 6;
    

    private int _currentIndexData;
    private int CurrentIndexData
    {
        get => _currentIndexData;
        set
        {
            if (data == null)
                _currentIndexData = 0;
            else
            {
                _currentIndexData = value == 0 ? 0 : value % data.Count;
                if (value < 0)
                    _currentIndexData += data.Count;
            }
        }
    }
    
    private int _currentIndexObjects;
    private int CurrentIndexObjects
    {
        get => _currentIndexObjects;
        set
        {
            _currentIndexObjects = value % ObjectsCount;
            if (value < 0)
                _currentIndexObjects += ObjectsCount;
        }
    }
    
    private float deltaScale;
    private Vector2 defaultSize;
    private Vector3 horizontalDeltaPositive = Vector3.zero;

    private List<Vector3> destinations = new();
    private int middleObjectIndex;

    private void OnValidate()
    {
        if (rectTransform == null)
        {
            rectTransform = transform as RectTransform ?? gameObject.AddComponent<RectTransform>();
        }
        rectTransform.sizeDelta = defaultSize * width;
    }

    private void Awake()
    {
        rectTransform = transform as RectTransform;
        
        for (var i = destinations.Count; i < ObjectsCount; i++)
            destinations.Add(default);
        for (var i = transperentObjects.Count; i < ObjectsCount; i++)
            transperentObjects.Add(default);
    }

    private IEnumerable PlaySlideAnimation(bool toRight)
    {
        var curSelected = transperentObjects[CurrentIndexObjects];
        var destination = curSelected.transform.position;
        var scaleDelta = new Vector3(deltaScale, deltaScale, deltaScale);

        ShiftObjectsIndexes(toRight);
        while (curSelected.transform.position != destination)
        {
            var positiveScaledCount = ObjectsCount % 2 == 0 ? ObjectsCount - 2 : ObjectsCount - 1;
            
            for (var i = 0; i < positiveScaledCount; i++)
            {
                var transform1 = transperentObjects[i].transform;
                var positionDelta = transform1.position - destinations[i];
                transform1.position += positionDelta * Time.deltaTime;
                transperentObjects[i].Transparency += scaleDelta.x * Time.deltaTime;
            }
            
            var transform2 = transperentObjects[^1].transform;
            var positionDelta2 = transform2.position - destinations[^1];
            transperentObjects[^1].transform.position -= positionDelta2*Time.deltaTime;
            transperentObjects[^1].Transparency += scaleDelta.x * Time.deltaTime;
            
            for (var i = 1; i < middleObjectIndex; i++)
            {
                transperentObjects[i].transform.localScale -= scaleDelta*Time.deltaTime;
                transperentObjects[-i].transform.localScale -= scaleDelta*Time.deltaTime;
            }

            if (ObjectsCount % 2 == 0)
            {
                transform2 = transperentObjects[^2].transform;
                positionDelta2 = transform2.position - destinations[^2];
                transperentObjects[^2].transform.position -= positionDelta2*Time.deltaTime;
                transperentObjects[^2].Transparency += scaleDelta.x * Time.deltaTime;
                transperentObjects[^1].transform.localScale -= scaleDelta*Time.deltaTime;
            }

            yield return new WaitForEndOfFrame();
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

        FillDestinations();

        if(transperentObjects is {Count: > 0})
            for (var i = 0; i < ObjectsCount; i++)
                Destroy(transperentObjects[i]);
        
        for (var i = 0; i < ObjectsCount; i++)
            transperentObjects[i] = Instantiate(prefub, transform);
        
        CurrentIndexObjects = 2;
        var scaleDelta = new Vector3(deltaScale, deltaScale, deltaScale);
        
        var positiveScaledCount = ObjectsCount % 2 == 0 ? ObjectsCount - 2 : ObjectsCount - 1;
            
        for (var i = 0; i < ObjectsCount; i++)
        {
            transperentObjects[i].transform.position = destinations[i];
        }

        transperentObjects[middleObjectIndex].Transparency = 1;
        for (var i = 1; i < middleObjectIndex; i++)
        {
            transperentObjects[middleObjectIndex+i].transform.localScale -= scaleDelta*Time.deltaTime;
            transperentObjects[middleObjectIndex+i].Transparency = 1 - (i*scaleDelta.x*Time.deltaTime);
            transperentObjects[middleObjectIndex-i].transform.localScale -= scaleDelta*Time.deltaTime;
            transperentObjects[middleObjectIndex-i].Transparency = 1 - (i*scaleDelta.x*Time.deltaTime);
        }

        if (ObjectsCount % 2 == 0)
        {
            transperentObjects[^2].Transparency += scaleDelta.x * Time.deltaTime;
            transperentObjects[^1].transform.localScale -= scaleDelta*Time.deltaTime;
        }

        if (data != null)
        {
            LoadFromCurrentAlignedIndex();
        }
    }

    private void FillDestinations()
    {
        defaultSize = prefub.transform.GetComponent<RectTransform>().rect.size;
        
        middleObjectIndex = ObjectsCount / 2;
        var objectsInFront = 0;

        if (ObjectsCount % 2 != 0)
        {
            middleObjectIndex++;
            objectsInFront = ObjectsCount - 1;
        }
        else
        {
            destinations[^1] = new(0, 0, -1);
            objectsInFront = ObjectsCount;
        }
        
        deltaScale = defaultSize.x * width / objectsInFront;
        horizontalDeltaPositive.x = defaultSize.x * deltaScale;
        destinations[middleObjectIndex] = new(0, 0, 0);

        horizontalDeltaPositive.x = width / objectsInFront;
        for (var i = 1; i < middleObjectIndex; i++)
        {
            destinations[middleObjectIndex-i] = Vector3.zero - horizontalDeltaPositive * i;
            destinations[middleObjectIndex+i] = Vector3.zero - horizontalDeltaPositive * i;
        }
    }

    public void LoadData([NotNull] Action<T,E> setDataFunction, [NotNull] List<E> data)
    {
        this.setDataFunction = setDataFunction;
        this.data = data;
        deltaScale = 0.1f;
        CurrentIndexData = -2;

        LoadFromCurrentAlignedIndex();
    }

    private void LoadFromCurrentAlignedIndex()
    {
        var objCount = 0;
        
        for (var i = CurrentIndexData; objCount < ObjectsCount; i++, objCount++)
        {
            var dataIndex = i%data.Count;
            setDataFunction(transperentObjects[objCount], data[dataIndex]);
        }
    }

    public void SwitchLeft()
    {
        CurrentIndexData--;
        LoadFromCurrentAlignedIndex();
        StartCoroutine("PlaySlideAnimation", false);
    }

    public void SwitchRight()
    {
        CurrentIndexData++;
        LoadFromCurrentAlignedIndex();
        StartCoroutine("PlaySlideAnimation", true);
    }

    public void SelectByIndex(int index)
    {
        var lastIndex = CurrentIndexData;
        CurrentIndexData = index+2;
        LoadFromCurrentAlignedIndex();
        StartCoroutine("PlaySlideAnimation", lastIndex < CurrentIndexData);
    }
}
