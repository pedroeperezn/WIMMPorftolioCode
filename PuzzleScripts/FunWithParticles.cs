using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FunWithParticles : MonoBehaviour
{
    private PuzzleManager _puzzleManager;
    private ItemManager _itemManager;
    private VisualEffect sparkles;
    [SerializeField] private PreasurePlateManager _pressurePlateManager;

    [Header("PuzzleManager")]
    [SerializeField] private bool _onIcarus;
    [SerializeField] private bool _onHusk1;
    [SerializeField] private bool _onLever;
    [SerializeField] private bool _onPressureplate1;
    [SerializeField] private bool _onKalimbadoor1;
    [SerializeField] private bool _onPressureplate2;
    [SerializeField] private bool _onCheckerboard;

    [Header("ItemManager")]
    [SerializeField] private bool _onKalimba;

    [Header("PressurePlates :)")]
    [SerializeField] private bool _inRoom2;
    [SerializeField] private bool _inRoom3;





    void Start()
    {
        _puzzleManager = PuzzleManager.PMInstance;
        _itemManager = ItemManager.Instance;
        sparkles = GetComponent<VisualEffect>();
    }

    
    void FixedUpdate()
    {
        // let it be known There are probably a million better ways to do this but this is the way that I could think of know
/*        if (_puzzleManager.IcarusSolved)
        {
            if (_onIcarus) { Object.Destroy(sparkles); }
        }


        if (_puzzleManager.Husk1HasMoved)
        {
            if (_onHusk1) { Object.Destroy(sparkles); }
        }

        if (_puzzleManager.LeaverSolved)
        {
            if (_onLever) { Object.Destroy(sparkles); }
        }

        if (_puzzleManager.FloorNotesSolved)
        {
            if (_onFloornotes) { Object.Destroy(sparkles); }
        }

        if (_puzzleManager.PressurePlate1Solved)
        {
            if (_onPressureplate2) { Object.Destroy(sparkles); }
        }

        if (_puzzleManager.KalimbaDoor1Solved)
        {
            if (_onKalimbadoor1) { Object.Destroy(sparkles); }
        }

        if (_puzzleManager.PressurePlate2Solved)
        {
            if (_onPressureplate2) { Object.Destroy(sparkles); }
        }

        if (_puzzleManager.CheckerboardSolved)
        {
            if (_onCheckerboard) { Object.Destroy(sparkles); }
        }

        if (_itemManager._kalimbaAquired)
        {
            if (_onKalimba)
            {
                Object.Destroy(sparkles);
            }
        }
        
        
        
        if (_pressurePlateManager.isSolved)
        {
            if (_inRoom2)
            {
                Object.Destroy(sparkles);
            }
            else if (_inRoom3) { Object.Destroy(sparkles);}
        }*/
        
       



    }
}
