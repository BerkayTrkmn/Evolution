using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelloScripts
{
    public class Library : MonoBehaviour
    {

        public static void DestroyAllChildWithT<T>(Transform parentTrans) where T : Component
        {
            T[] components = parentTrans.GetComponentsInChildren<T>();

            foreach (T component in components)
            {
                Destroy(component.gameObject);
            }
        }

        ///For ragdoll character explosion
        #region Character Explosion

        /// <summary>
        /// Close/Open all child rigidbody's
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="activity">is kinematic activity</param>
        /// <param name="xforce"></param>
        /// <param name="yforce"></param>
        /// <param name="zforce"></param>
        /// <param name="isForced">is force applies?</param>

        public static void ChangeActiveRBAllChildren(Transform parent, bool activity, float xforce, float yforce, float zforce, bool isForced = true)
        {
            Rigidbody[] rbs = parent.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = activity;
                if (isForced) rb.AddForce(xforce, yforce, zforce);
            }
        }
        /// <summary>
        /// Close/Open all child colliders
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="activity"></param>
        public static void ChangeActivityAllColliderInChildren(Transform parent, bool activity)
        {
            Collider[] colliders = parent.GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = activity;
            }
        }

        /// <summary>
        /// Ragdoll transform explodes
        /// </summary>
        /// <param name="explodedCharacter"></param>
        /// <param name="explosionMultiplier">Vector of explosion (direction and magnitude) </param>
        public static void CharacterExplosion(Transform explodedCharacter, Vector3 explosionMultiplier)
        {
            ChangeActiveRBAllChildren(explodedCharacter.transform, false, 1000 * explosionMultiplier.x, 1000 * explosionMultiplier.z, 1000 * explosionMultiplier.z);
            ChangeActivityAllColliderInChildren(explodedCharacter, true);
            explodedCharacter.transform.GetComponent<Animator>().enabled = false;
            explodedCharacter.transform.parent = null;
            foreach (Collider col in explodedCharacter.GetComponents<Collider>())
                col.enabled = false;

        }
        /// <summary>
        /// Vector of two transform
        /// </summary>
        /// <param name="startTrans"></param>
        /// <param name="endTrans"></param>
        /// <returns></returns>
        public static Vector3 TransformVector(Transform startTrans, Transform endTrans)
        {
            return endTrans.position - startTrans.position;
        }
        #endregion
        /// <summary>
        /// Vector of two point
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static Vector3 TransformVector(Vector3 startPoint, Vector3 endPoint)
        {
            return endPoint - startPoint;
        }

        public static void CreateGameObjectandPlaceIt(GameObject createdGO, Transform trans)
        {
            GameObject go = Instantiate(createdGO);

            go.transform.position = trans.position;
        }
    }
}