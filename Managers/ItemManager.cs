using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ItemManager Instance;
    [SerializeField] private List<GameObject> AqcuiredItems;
    [SerializeField] private GameObject _kalimbaEquipedUI;

    public bool _kalimbaAquired = false;

    void Start()
    {
    }

    private void Awake()
    {
        Instance = this;
        AqcuiredItems = new List<GameObject>();
        
    }

    public void Add(GameObject itemToAdd)
    {
        AqcuiredItems.Append(itemToAdd);
        itemToAdd.SetActive(true);
    }

    void Update()
    {
/*        foreach (GameObject item in AqcuiredItems)
        { 
            item.SetActive(true);
        }

        if(_kalimbaAquired) 
        {
            _kalimbaEquipedUI.SetActive(true);
        }*/
        
    }
}
