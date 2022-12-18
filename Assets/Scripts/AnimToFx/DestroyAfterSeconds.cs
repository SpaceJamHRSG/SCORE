using System;
using System.Collections;
using UnityEngine;

namespace AnimToFx
{
    public class DestroyAfterSeconds : MonoBehaviour
    {
        [SerializeField] private float _lifetime;
        private void Start()
        {
            StartCoroutine(DestructionSequence(_lifetime));
        }

        IEnumerator DestructionSequence(float t)
        {
            yield return new WaitForSeconds(t);
            Destroy(gameObject);
        }
    }
}