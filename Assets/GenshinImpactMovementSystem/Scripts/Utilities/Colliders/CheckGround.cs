using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class CheckGround : MonoBehaviour
    {
        public bool IsOnGround;
        [field: SerializeField] public PlayerLayerData LayerData { get; private set; }
        public virtual void OnTriggerEnter(Collider collider)
        {

            if (LayerData.IsGroundLayer(collider.gameObject.layer))
            {
                IsOnGround = true;

                return;
            }
        }

        public virtual void OnTriggerExit(Collider collider)
        {
            if (LayerData.IsGroundLayer(collider.gameObject.layer))
            {
                IsOnGround = false;

                return;
            }
        }

    }
}
