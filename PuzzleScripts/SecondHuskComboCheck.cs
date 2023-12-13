using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondHuskComboCheck : MonoBehaviour
{
    [SerializeField] private GameObject _Bench;
    [SerializeField] private GameObject _pressurePlate;
    [SerializeField] private GameObject _LeaverForHusk;
    [SerializeField] private float movementSpeed;

    private Vector3 _huskInitPosition;

    private GameManager gameManager;
    private PuzzleManager PuzzleManager;
    private Rigidbody HuskRigidBody;
    private AudioManager audioManager;
    private FMODEvents fmod;

    public bool CanMove = false;

    public bool isInBench = true;
    public bool isInPressurePlate = false;


    public void SecondHuskComboOk()
    {
        
        if (this.GetComponent<ComboCheck>().comboTriggered && isInBench && CanMove)
        {
            //audioManager.PlayOneShot(fmod.CorrectAnswer, transform.position);


            PuzzleManager.SecondHuskSparkles.SetActive(false);

            float distanceToLeaver = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_Bench.transform.position.x, _Bench.transform.position.z));
            float distanceToPPlate = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_pressurePlate.transform.position.x, _pressurePlate.transform.position.z));

            if (distanceToPPlate > 0.01)
            {
                Vector3 direction = new Vector3(_pressurePlate.transform.position.x - transform.position.x, 0f,
                                            _pressurePlate.transform.position.z - transform.position.z).normalized;

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_pressurePlate.transform.position.x, transform.position.y,
                                                                 _pressurePlate.transform.position.z), movementSpeed * Time.deltaTime);

                //isInLeaver = true;

                HuskRigidBody.MovePosition(transform.position + direction * Time.deltaTime * movementSpeed);

                Quaternion currentRotation = transform.rotation;
                Quaternion targetRotation = Quaternion.LookRotation(direction); // converts a direction (Vector3) to a rotation (Quaternion)
                Quaternion rotation = Quaternion.Slerp(currentRotation, targetRotation, 1f);
                transform.rotation = rotation;

            }

        }

        else if (this.GetComponent<ComboCheck>().comboTriggered && !isInBench && CanMove)
        {
            //audioManager.PlayOneShot(fmod.CorrectAnswer, transform.position);


            float distanceToLeaver = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_Bench.transform.position.x, _Bench.transform.position.z));
            float distanceToPPlate = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_pressurePlate.transform.position.x, _pressurePlate.transform.position.z));

            if (distanceToLeaver > 0.01)
            {
                Vector3 direction = new Vector3(_Bench.transform.position.x - transform.position.x, 0f,
                                            _Bench.transform.position.z - transform.position.z).normalized;

/*                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_leaver.transform.position.x, transform.position.y,
                                                                 _leaver.transform.position.z), movementSpeed * Time.deltaTime);*/

                HuskRigidBody.MovePosition(transform.position + direction * Time.deltaTime * movementSpeed);

                //isInLeaver = true;
                Quaternion currentRotation = transform.rotation;
                Quaternion targetRotation = Quaternion.LookRotation(direction); // converts a direction (Vector3) to a rotation (Quaternion)
                Quaternion rotation = Quaternion.Slerp(currentRotation, targetRotation, 1f);
                transform.rotation = rotation;

            }

        }

        else if(this.GetComponent<ComboCheck>().comboTriggered && !CanMove)
        {

            this.GetComponent<ComboCheck>().comboTriggered = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Leaver")
        {
            Debug.Log("Husk in Leaver");
            isInBench = true;
            PuzzleManager.Husk1HasMoved = true;
            this.GetComponent<ComboCheck>().comboTriggered = false;
        }

        else if (other.gameObject.tag == "Plate")
        {
            Debug.Log("Husk in Plate");
            isInBench = false;
            isInPressurePlate = true;
            PuzzleManager.Husk1HasMoved = true;
            this.GetComponent<ComboCheck>().comboTriggered = false;
        }
    }



    private void FixedUpdate()
    {
        CanMove = _LeaverForHusk.GetComponent<OpenDoorInLeaver>()._doorIsOpen;
        SecondHuskComboOk();
    }

    private void Start()
    {
        //Not Getting the HuskSolved Variable
        PuzzleManager = PuzzleManager.PMInstance;
        gameManager = GameManager.Instance;
        _huskInitPosition = transform.position;
        HuskRigidBody = GetComponent<Rigidbody>();
        audioManager = AudioManager.AMInstance;
        fmod = FMODEvents.FMInstance;
    }
}
