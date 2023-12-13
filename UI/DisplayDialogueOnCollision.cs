using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDialogueOnCollision : MonoBehaviour
{

    [SerializeField]  public GameObject DialogueScreen;
    private PuzzleManager PuzzleManager;
    public bool isShowable = true;
    
    // Start is called before the first frame update
/*    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character") && isShowable)
        {
            DialogueScreen.SetActive(true);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character") && isShowable)
        {
            DialogueScreen.SetActive(true);
        }
    }

/*    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            DialogueScreen.SetActive(false);
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            DialogueScreen.SetActive(false);
        }
    }

    private void Start()
    {
        PuzzleManager = PuzzleManager.PMInstance;
    }
}
