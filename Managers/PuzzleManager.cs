using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager PMInstance;

    //GameObjects with puzzles 
    [Header("Game Elements")]
    [SerializeField] private GameObject kalimba;
    [SerializeField] private GameObject checkers;
    [SerializeField] private GameObject Door1;
    [SerializeField] private GameObject CheckerTrigger;
    [SerializeField] private GameObject Door2;


    //Visual feedback for unsolved puzzles
    [Header("Puzzle Sparkles")]
    [SerializeField] public GameObject IcarusSparkles;
    [SerializeField] public GameObject Husk1Sparkles;
    [SerializeField] public GameObject BarDoor1Sparkles;
    [SerializeField] public GameObject PPlate1_1Sparkles;
    [SerializeField] public GameObject PPlate1_2Sparkles;
    [SerializeField] public GameObject KalimbaDoor1Sparkles;
    [SerializeField] public GameObject Leaver2Sparkles;
    [SerializeField] public GameObject SecondHuskSparkles;
    [SerializeField] public GameObject PPlate2_1Sparkles;
    [SerializeField] public GameObject PPlate2_2Sparkles;
    [SerializeField] public GameObject ChestSparkles;
    [SerializeField] public GameObject CheckersSparkles;
    [SerializeField] public GameObject FinalDoorSparkles;

    [Header("Interactable Sparkles")]
    [SerializeField] public GameObject CollectKalimbaSparkles;
    [SerializeField] public GameObject FirstSheetSparkles;
    [SerializeField] public GameObject CollectCheckersSparkles;

    //Debug tools
    [Header("Debug Controllers")]
    [SerializeField] private InputActionReference _debugIcarus;
    [SerializeField] private InputActionReference _openRoom1Door;
    [SerializeField] private InputActionReference _restartScene;
    [SerializeField] private InputActionReference _showChecker;
    [SerializeField] private InputActionReference _solveChecker;
    [SerializeField] private InputActionReference _openFinalDoor;

    //Checks for solved puzzzles
    public bool IcarusSolved = false;
    public bool Husk1HasMoved = false;
    public bool BarDoor1Opened = false;
    public bool PressurePlate1Solved = false;
    private bool isDoor1Open = false;
    public bool KalimbaDoor1Solved = false;
    public bool PressurePlate2Solved = false;

    //checkerboard bool new reference
    public bool CheckerboardSolved = false;
    public bool finalDoorSolved = false;

    //Managers
    AudioManager audioManager;
    FMODEvents fmod;
    private bool kalimbaHasShown;

    private void Start()
    {
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }

    private void Awake()
    {
        PMInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Enable kalimba after icarus puzzle is solved
        if (IcarusSolved)
        {
            kalimba.GetComponent<SphereCollider>().enabled = true;
            IcarusSparkles.SetActive(false);
            if (!kalimbaHasShown) 
            {
                CollectKalimbaSparkles.SetActive(true);
                kalimbaHasShown = true;
            }
        }

    }

    //automatically solve icarus
    private void OnDebugIcarus(InputAction.CallbackContext obj)
    {
        IcarusSolved = true;
    }

    //skip first room
    private void OnOpenDoorOne(InputAction.CallbackContext obj)
    {
        if (!isDoor1Open)
        {
            Door1.GetComponent<Animator>().SetTrigger("Open");
            isDoor1Open = true;
        }

        else
        {
            Door1.GetComponent<Animator>().SetTrigger("Close");
            isDoor1Open = false;

        }
    }

    private void OnRestartScene(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnShowChecker(InputAction.CallbackContext obj)
    {
        
        CheckerTrigger.gameObject.SetActive(!CheckerTrigger.activeSelf);
    }
    private void OnSolveChecker(InputAction.CallbackContext obj)
    {
        CheckerboardSolved = !CheckerboardSolved;
    }

    //open final door automatically
    private void OnOpenFinalDoor(InputAction.CallbackContext obj)
    {
        Door2.GetComponent<Animator>().SetTrigger("Open");
    }

    //enable input actions
    private void OnEnable()
    {
        _debugIcarus.action.performed += OnDebugIcarus;
        _openRoom1Door.action.performed += OnOpenDoorOne;
        _restartScene.action.performed += OnRestartScene;
        _showChecker.action.performed += OnShowChecker;
        _solveChecker.action.performed += OnSolveChecker;
        _openFinalDoor.action.performed += OnOpenFinalDoor;
    }

    //disable input actions
    private void OnDisable()
    {
        _debugIcarus.action.performed -= OnDebugIcarus;
        _openRoom1Door.action.performed -= OnOpenDoorOne;
        _restartScene.action.performed -= OnRestartScene;
        _showChecker.action.performed -= OnShowChecker;
        _solveChecker.action.performed -= OnSolveChecker;
        _openFinalDoor.action.performed -= OnOpenFinalDoor;

    }

}
