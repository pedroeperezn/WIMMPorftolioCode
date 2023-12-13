/*using Sirenix.OdinInspector.Editor.GettingStarted;
using System.Collections;*/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using FMODUnity;
//using Sirenix.OdinInspector.Editor.GettingStarted;

public class MoveImageInUI : MonoBehaviour, IDragHandler, IEndDragHandler
{

    [SerializeField] private Image ImageToDrag;
    [SerializeField] private Image TargetImage;
    [SerializeField] private GraphicRaycaster _rayCaster;

    [SerializeField] private GameObject PuzzlePanel;

    [SerializeField] private float DropDistance;
    [SerializeField] private bool isLocked;

    private Vector2 ImageInitPosition;
    private ImagePuzzleManager IcarusPuzzleManager;
    private GameObject[] Placeholders;

    private bool notMoved = true;

    public GameObject CurrentTarget { get; private set; }

    //Managers
    AudioManager audioManager;
    FMODEvents fmod;


    private void Start()
    {
        ImageInitPosition = ImageToDrag.transform.position;
        IcarusPuzzleManager = PuzzlePanel.GetComponent<ImagePuzzleManager>();
        Placeholders = IcarusPuzzleManager.imageArray;
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }

    public void PlayDragSound() 
    {
        if (PuzzlePanel.GetComponent<ImagePuzzleManager>().isIcarus)
        {
            audioManager.PlayOneShot(fmod.IcarusDragImage, transform.position);
        }

        else if (PuzzlePanel.GetComponent<ImagePuzzleManager>().isCheckerboard)
        {
            audioManager.PlayOneShot(fmod.CheckerDragImage, transform.position);
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (!isLocked)
        {
            this.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PointerEventData pData = new PointerEventData(EventSystem.current);
        pData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        _rayCaster.Raycast(pData, results);
        foreach (var item in results)
        {
            if (Placeholders.Contains(item.gameObject))
            {
                CurrentTarget = item.gameObject;
                notMoved = false;
            }

        }

        if(!notMoved)
        {
            if (PuzzlePanel.GetComponent<ImagePuzzleManager>().isIcarus)
            {
                audioManager.PlayOneShot(fmod.IcarusDropImage, transform.position);
            }

            else if (PuzzlePanel.GetComponent<ImagePuzzleManager>().isCheckerboard)
            {
                audioManager.PlayOneShot(fmod.CheckerDropImage, transform.position);
            }

            //audioManager.PlayOneShot(fmod.IcarusDropImage, transform.position);

            if (CurrentTarget.tag == "IcarusImagePlaceHolder")
            {
                CurrentTarget.GetComponent<Image>().sprite = ImageToDrag.sprite;
                ImageToDrag.transform.position = ImageInitPosition;
            }

            else if (CurrentTarget.tag == "UI")
            {
                ImageToDrag.transform.position = ImageInitPosition;
            }

            else
            {
                ImageToDrag.transform.position = ImageInitPosition;
            }
        }

    }

    
}
