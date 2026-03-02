using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ResourceStorage))]

public class StorageChecker : MonoBehaviour
{
    private readonly int BaseRequiredResourcesAmount = 0;

    private ResourceStorage _storage;

    private Dictionary<ResourceType, int> _requiredResources;

    public event Action GoalSet;

    private void Awake()
    {
        _storage = GetComponent<ResourceStorage>();

        _requiredResources = new();
        ResetRequirements();
    }

    private void OnEnable()
    {
        ResourceStorageSlot slot;

        foreach (ResourceType type in _requiredResources.Keys)
        {
            slot = _storage.GetSlot(type);

            if (slot != null)
                slot.AmountChanged += UpdateRequiredAmount;
        }
    }

    private void OnDisable()
    {
        ResetRequirements();
    }

    private void Start()
    {
        SetGoalFillStorage();
    }

    public Dictionary<ResourceType, int> GetRequiredResources()
    {
        Dictionary<ResourceType, int> requiredResourcesCopy = new();

        requiredResourcesCopy.AddRange(_requiredResources);

        return requiredResourcesCopy;
    }

    public int GetRequiredAmount(ResourceType resourceType)
    {
        if (_requiredResources.ContainsKey(resourceType))
        {
            return _requiredResources[resourceType];
        }
        else
        {
            return BaseRequiredResourcesAmount;
        }
    }

    private void ResetRequirements()
    {
        ResourceStorageSlot slot;

        foreach (ResourceType type in _requiredResources.Keys)
        {
            slot = _storage.GetSlot(type);

            if (slot != null)
                slot.AmountChanged -= UpdateRequiredAmount;
        }

        _requiredResources.Clear();
    }

    private void SetGoalFillStorage()
    {
        ResetRequirements();
        
        foreach (ResourceStorageSlot slot in _storage.GetAllSlots())
        {
            if (slot.Capacity - slot.Amount > 0)
            {
                _requiredResources.Add(slot.Type, slot.Capacity - slot.Amount);
                slot.AmountChanged += UpdateRequiredAmount;
            }
        }

        GoalSet?.Invoke();
    }

    private void UpdateRequiredAmount(ResourceStorageSlot storageSlot, int change)
    {
        _requiredResources[storageSlot.Type] -= change;
    }
}
