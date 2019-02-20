using UnityEngine;

namespace EZObjectPools
{
    [AddComponentMenu("EZ Object Pools/Pooled Object")]
    public class PooledObject : MonoBehaviour
    {
        /// <summary>
        /// The object pool this object originated from.
        /// </summary>
        [HideInInspector]
        public EZObjectPool ParentPool;

        [SerializeField]
        private Robot robot;
        /// <summary>
        /// [OBSOLETE] Simply calls gameObject.SetActive(false). No longer needed in your scripts.
        /// </summary>
        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }
        public virtual void Awake()
        {
            robot = GetComponent<Robot>();
        }

        void OnDisable()
        {

            if (robot != null)
            {

                if (!robot.recluted)
                {

                    OnDisablePoolStuff();
                }
                else
                {
                }
            }
            else
            {

                OnDisablePoolStuff();
            }
        }


        private void OnDisablePoolStuff()
        {
            transform.position = Vector3.zero;

            if (ParentPool)
            {
                ParentPool.AddToAvailableObjects(gameObject);
            }
            else
            {
                Debug.LogWarning("PooledObject " + gameObject.name + " does not have a parent pool. If this occurred during a scene transition, ignore this. Otherwise reoprt to developer.");
            }
        }
    }
}