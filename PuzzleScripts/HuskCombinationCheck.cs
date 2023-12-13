using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HuskCombinationCheck : MonoBehaviour
{
    [SerializeField] private GameObject _leaver;
    [SerializeField] private GameObject _pressurePlate;
    [SerializeField] private GameObject _barDoor;
    [SerializeField] private float movementSpeed;

    private Vector3 _huskInitPosition;

    private GameManager gameManager;
    private PuzzleManager PuzzleManager;
    private AudioManager audioManager;
    private FMODEvents fmod;
    private Rigidbody HuskRigidBody;

    public bool CanMove = true;

    public bool huskHasMoved = false;

    public bool isInLeaver = false;
    public bool isInPressurePlate = false;
    

    public void HuskComboOk()
    {
        //husk1 movement to point A
        if (this.GetComponent<ComboCheck>().comboTriggered && !huskHasMoved && CanMove)
        {
            //indicates that the husk has moved
            PuzzleManager.Husk1HasMoved = true;
            PuzzleManager.Husk1Sparkles.SetActive(false);
            this.GetComponentInChildren<DisplayDialogueOnCollision>().DialogueScreen.SetActive(false);

            //diable kalimba for player to see the action
            if (!huskHasMoved)
            {
                gameManager.DisableKalimba();
            }

            //Dont display the melody anymore
            this.GetComponentInChildren<DisplayDialogueOnCollision>().isShowable = false;

            //get distance to lever and move
            float distanceToLeaver = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_leaver.transform.position.x, _leaver.transform.position.z));
            float distanceToPPlate = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_pressurePlate.transform.position.x, _pressurePlate.transform.position.z));

            //move husk 
            if (distanceToLeaver > 0.01 && !huskHasMoved)
            {
                Vector3 direction = new Vector3(_leaver.transform.position.x - transform.position.x, 0f,
                                            _leaver.transform.position.z - transform.position.z).normalized;
                HuskRigidBody.MovePosition(transform.position + direction * Time.deltaTime * movementSpeed);

                Quaternion currentRotation = transform.rotation;
                Quaternion targetRotation = Quaternion.LookRotation(direction); // converts a direction (Vector3) to a rotation (Quaternion)
                Quaternion rotation = Quaternion.Slerp(currentRotation, targetRotation, 1f);
                transform.rotation = rotation;
                isInLeaver = true;

            }

        }

        //husk 1 movement to point b
        else if (this.GetComponent<ComboCheck>().comboTriggered && huskHasMoved && isInLeaver && CanMove)
        {
            //check distance
            float distanceToLeaver = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_leaver.transform.position.x, _leaver.transform.position.z));
            float distanceToPPlate = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_pressurePlate.transform.position.x, _pressurePlate.transform.position.z));

            //move to preassure plate (point B)
            if (distanceToPPlate > 0.01)
            {
                Vector3 direction = new Vector3(_pressurePlate.transform.position.x - transform.position.x, 0f,
                                            _pressurePlate.transform.position.z - transform.position.z).normalized;

                HuskRigidBody.MovePosition(transform.position + direction * Time.deltaTime * movementSpeed);

                Quaternion currentRotation = transform.rotation;
                Quaternion targetRotation = Quaternion.LookRotation(direction); // converts a direction (Vector3) to a rotation (Quaternion)
                Quaternion rotation = Quaternion.Slerp(currentRotation, targetRotation, 1f);
                transform.rotation = rotation;

            }

        }

        //husk2 behavior, this one doesn't return to the original position
        else if (this.GetComponent<ComboCheck>().comboTriggered && huskHasMoved && !isInLeaver && CanMove)
        {
            float distanceToLeaver = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_leaver.transform.position.x, _leaver.transform.position.z));
            float distanceToPPlate = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_pressurePlate.transform.position.x, _pressurePlate.transform.position.z));

            if (distanceToLeaver > 0.01)
            {
                Vector3 direction = new Vector3(_leaver.transform.position.x - transform.position.x, 0f,
                                            _leaver.transform.position.z - transform.position.z).normalized;



                HuskRigidBody.MovePosition(transform.position + direction*Time.deltaTime*movementSpeed);
                Quaternion currentRotation = transform.rotation;
                Quaternion targetRotation = Quaternion.LookRotation(direction); // converts a direction (Vector3) to a rotation (Quaternion)
                Quaternion rotation = Quaternion.Slerp(currentRotation, targetRotation, 1f);
                transform.rotation = rotation;

            }

        }



    }

    private void OnTriggerEnter(Collider other)
    {
        //Point A
        if (other.gameObject.tag == "Leaver")
        {
            huskHasMoved = true;
            isInLeaver = true;
            audioManager.PlayOneShot(fmod.barDoor,_barDoor.transform.position);
            this.GetComponent<ComboCheck>().comboTriggered = false;
        }
        
        //Point B
        else if (other.gameObject.tag == "Plate")
        {
            isInLeaver = false;
            isInPressurePlate = true;
            this.GetComponent<ComboCheck>().comboTriggered = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Leaver")
        {
            audioManager.PlayOneShot(fmod.barDoor, _barDoor.transform.position);
        }
    }



    private void FixedUpdate()
    {
        HuskComboOk();
    }

    private void Start()
    {
        //Not Getting the HuskSolved Variable
        PuzzleManager = PuzzleManager.PMInstance;
        gameManager = GameManager.Instance;
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
        _huskInitPosition = transform.position;
        HuskRigidBody = GetComponent<Rigidbody>();
    }
}
