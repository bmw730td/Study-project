using System.Collections.Generic;
using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(PickUpAbleEffects))]
    
    public class PlayerPickUp : MonoBehaviour
    {
        [SerializeField] private PickUpAbleEffects _pickUpAbleEffects;

        [SerializeField, Min(0f)] private float _pickUpAttractionSpeed;
        [SerializeField, Min(0f)] private float _pickUpAttractionDistance;
        [SerializeField, Min(0f)] private float _pickUpDistance;

        private void OnValidate()
        {
            if (_pickUpDistance > _pickUpAttractionDistance)
                _pickUpDistance = _pickUpAttractionDistance;
        }

        private void Update()
        {
            foreach(PickUpAble pickUpAble in GetPickUpAbles())
            {
                pickUpAble.transform.position = Vector3.MoveTowards(pickUpAble.transform.position, transform.position, _pickUpAttractionSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, pickUpAble.transform.position) <= _pickUpDistance)
                {
                    _pickUpAbleEffects.ApplyEffect(pickUpAble.Type);
                    Destroy(pickUpAble.gameObject);
                }
            }
        }

        private List<PickUpAble> GetPickUpAbles()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _pickUpAttractionDistance);

            List<PickUpAble> pickUpAbles = new();
            PickUpAble tempPickUpAble;

            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent<PickUpAble>(out tempPickUpAble))
                {
                    pickUpAbles.Add(tempPickUpAble);
                }
            }

            return pickUpAbles;
        }
    }
}
