using System.Collections.Generic;
using UnityEngine;

namespace BattleForPlatformer
{
    public class Collector : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _attractionRange;
        [SerializeField, Min(0f)] private float _attractionSpeed;
        [SerializeField, Min(0f)] private float _collectionRange;

        private List<CollectableItem> _collectableItemsInRange = new();

        private void Update()
        {
            CheckCollectableItemsInRange();

            foreach (CollectableItem collectableItem in _collectableItemsInRange)
            {
                collectableItem.transform.position = Vector3.MoveTowards(collectableItem.transform.position, transform.position, _attractionSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, collectableItem.transform.position) <= _collectionRange)
                    collectableItem.Collect(gameObject);
            }
        }

        private void CheckCollectableItemsInRange()
        {
            Collider2D[] collider2DHits = Physics2D.OverlapCircleAll(transform.position, _attractionRange);

            _collectableItemsInRange.Clear();

            foreach (Collider2D collider2DHit in collider2DHits)
            {
                if (collider2DHit.TryGetComponent(out CollectableItem tempCollectableItem))
                    _collectableItemsInRange.Add(tempCollectableItem);
            }
        }
    }
}
