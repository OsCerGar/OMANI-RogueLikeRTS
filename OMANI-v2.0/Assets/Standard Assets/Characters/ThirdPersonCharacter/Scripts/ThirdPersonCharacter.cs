using UnityEngine;

using UnityEngine.AI;
namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
    {
        public  bool Rotate = false;
        [SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;
        Vector3 move;
		Rigidbody m_Rigidbody;
		Animator m_Animator;
		bool m_IsGrounded;
		float m_OrigGroundCheckDistance;
		float m_TurnAmount;
		float m_ForwardAmount;
        Animator anim;
        NavMeshAgent Nav;

        void Awake()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
            anim = gameObject.GetComponent<Animator>();
            Nav = gameObject.GetComponent<NavMeshAgent>();

            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}


		public void Move(Vector3 move)
		{
            if (Vector3.Distance (transform.position, move) > 0.2f)
            {

            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.//
            if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
            m_TurnAmount = move.x;
            m_ForwardAmount = move.z;
            if (Rotate)
            {
                ApplyExtraTurnRotation();
            }





            UpdateAnimator(move);
            }
        }
        

		


		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			m_Animator.SetFloat("Z", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("X", m_TurnAmount, 0.1f, Time.deltaTime);
			
            /*
			if (move.magnitude > 0)
			{
				m_Animator.speed = m_AnimSpeedMultiplier;
			}
            */
		}

        

		void ApplyExtraTurnRotation()
		{
            // help the character turn faster (this is in addition to root rotation in the animation)

            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}


		public void OnAnimatorMove()
		{
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            /*
				Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
                
				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
            */

            if (anim != null && Nav != null) { 
                Vector3 position = anim.rootPosition;
                position.y = Nav.nextPosition.y;
                transform.position = position;
                Nav.nextPosition = transform.position;
            }



        }
        
	}
}
