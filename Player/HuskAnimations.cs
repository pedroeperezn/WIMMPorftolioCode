using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterMovement
{
    public class HuskAnimations : MonoBehaviour
    {
        // damping time smooths rapidly changing values sent to animator
        [SerializeField] protected float _dampTime = 0.1f;

        protected Animator _animator;
        protected Rigidbody _characterMovement;
        private Vector3 _previousPosition;
        private Vector3 _transformVelocity;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterMovement = GetComponentInParent<Rigidbody>();
            _previousPosition = transform.position;
        }

        protected virtual void Update()
        {
            // send velocity to animator, ignoring y-velocity
            Vector3 velocity = _characterMovement.velocity;

            // check transform velocity
            if(_transformVelocity.magnitude > velocity.magnitude)
            {
                velocity = _transformVelocity;
            }

            Vector3 flattenedVelocity = new Vector3(velocity.x, 0f, velocity.z);
            float speed = Mathf.Min(flattenedVelocity.magnitude);
            _animator.SetFloat("Speed", speed, _dampTime, Time.deltaTime);
            
            // send isolated y-velocity
            _animator.SetFloat("VerticalVelocity", velocity.y);
        }

        private void FixedUpdate()
        {
            Vector3 movedVector = transform.position - _previousPosition;
            Vector3 velocity = movedVector / Time.fixedDeltaTime;
            _transformVelocity = velocity;
            _previousPosition = transform.position;
        }
    }
}
