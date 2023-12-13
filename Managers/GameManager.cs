using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UIComponents;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private PuzzleManager PMInstance;
    private InputActionListener _kalimbaInputListener;

    [SerializeField] private InputActionReference Collect;

    [SerializeField] private GameObject Kalimba;
    [SerializeField] private GameObject KalimbaKeyboard;
    [SerializeField] private GameObject KalimbaBackground;
    [SerializeField] private GameObject KalimbaTriggerAction;



    private SwitchActionMaps _switchActions;

    //I can get the objects with combination here instead than in the kalimba
    [SerializeField] public List<GameObject> ObjectsWithCombo;

    private bool _disableKalimba = false;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _switchActions = GetComponent<SwitchActionMaps>();
        _kalimbaInputListener = Kalimba.GetComponentInChildren<InputActionListener>();
        PMInstance = PuzzleManager.PMInstance;
        Kalimba.SetActive(false);
    }


    public void DisableKalimba() 
    {
        if (_kalimbaInputListener != null)
        {
            //Check how could this be done cleaner
            //_kalimbaInputListener.ForcePerform();

            KalimbaBackground.SetActive(false);
            KalimbaKeyboard.SetActive(false);
            KalimbaTriggerAction.SetActive(true);
            _switchActions.SwitchMap("Player");
            Debug.Log("Aja");
        }

        else 
        {
            Debug.Log("Why");
        }
    }

}
