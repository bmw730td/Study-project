using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceStorage))]

public class StorageChecker : MonoBehaviour
{
    private readonly int MinRequiredResourceTypesToReset = 0;
    private readonly int MaxResourceAmountToCompleteGoal = 0;

    [SerializeField] private List<GoalRequirements> _allGoalRequirements;

    private readonly int BaseRequiredResourcesAmount = 0;

    private ResourceStorage _storage;
    private Dictionary<ResourceType, int> _requiredResources;
    private bool _shouldRemoveResources;

    public event Action GoalSet;
    public event Action<BaseGoals> GoalDone;

    public BaseGoals CurrentGoal { get; private set; }

    private void Awake()
    {
        _storage = GetComponent<ResourceStorage>();

        _requiredResources = new();
    }

    private void OnEnable()
    {
        ResetRequirements(shouldBeSubscribed: true);
    }

    private void OnDisable()
    {
        ResetRequirements(shouldBeSubscribed: false);
    }

    public void SetGoal(BaseGoals goal)
    {
        CurrentGoal = goal;

        switch (CurrentGoal)
        {
            case BaseGoals.None:
                _shouldRemoveResources = false;
                ResetRequirements(shouldBeSubscribed: false);

                break;

            case BaseGoals.FillStorage:
                SetRequirementsToFillStorage();

                break;

            case BaseGoals.MakeBot:
                SetRequirements();

                break;

            case BaseGoals.BuildBase:
                SetRequirements();

                break;
        }

        GoalSet?.Invoke();
    }

    public Dictionary<ResourceType, int> GetRequiredResources()
    {
        return _requiredResources.ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public int GetRequiredAmount(ResourceType type)
    {
        if (_requiredResources.ContainsKey(type))
        {
            return _requiredResources[type];
        }
        else
        {
            return BaseRequiredResourcesAmount;
        }
    }

    public bool CheckIfStorageIsFull()
    {
        foreach (ResourceStorageSlot slot in _storage.GetAllSlots())
        {
            if (slot.Amount < slot.Capacity)
                return false;
        }

        return true;
    }

    private void ResetRequirements(bool shouldBeSubscribed)
    {
        if (_requiredResources.Count > MinRequiredResourceTypesToReset)
        {
            ResourceStorageSlot slot;

            foreach (ResourceType type in _requiredResources.Keys)
            {
                slot = _storage.GetSlot(type);

                if (slot != null)
                {
                    if (shouldBeSubscribed)
                    {
                        slot.AmountChanged += UpdateRequiredAmount;
                    }
                    else
                    {
                        slot.AmountChanged -= UpdateRequiredAmount;
                    }
                }
            }

            if (shouldBeSubscribed == false)
                _requiredResources.Clear();
        }
    }

    private void UpdateRequiredAmount(ResourceStorageSlot storageSlot, int change)
    {
        _requiredResources[storageSlot.Type] -= change;

        if (_requiredResources[storageSlot.Type] <= MaxResourceAmountToCompleteGoal)
            TryEndGoal();
    }

    private void TryEndGoal()
    {
        bool isGoalDone = true;

        foreach (int amount in _requiredResources.Values)
        {
            if (amount > 0)
                isGoalDone = false;
        }

        if (isGoalDone)
        {
            ResetRequirements(shouldBeSubscribed: false);

            if (_shouldRemoveResources)
                RemoveResources();

            GoalDone?.Invoke(CurrentGoal);
        }
    }

    private void RemoveResources()
    {
        Dictionary<ResourceType, int> resourcesToRemove = _allGoalRequirements.First(requirements => requirements.Goal == CurrentGoal).GetRequiredAmount();

        foreach (ResourceType type in resourcesToRemove.Keys)
        {
            _storage.RemoveResource(type, resourcesToRemove[type]);
        }
    }

    private void SetRequirementsToFillStorage()
    {
        _shouldRemoveResources = false;
        ResetRequirements(shouldBeSubscribed: false);

        foreach (ResourceStorageSlot slot in _storage.GetAllSlots())
        {
            if (slot.Capacity - slot.Amount > 0)
            {
                _requiredResources.Add(slot.Type, slot.Capacity - slot.Amount);
                slot.AmountChanged += UpdateRequiredAmount;
            }
        }

        TryEndGoal();
    }

    private void SetRequirements()
    {
        _shouldRemoveResources = true;
        ResetRequirements(shouldBeSubscribed: false);

        Dictionary<ResourceType, int> currentGoalRequirements = _allGoalRequirements.First(requirements => requirements.Goal == CurrentGoal).GetRequiredAmount();

        foreach (ResourceType type in currentGoalRequirements.Keys)
        {
            _requiredResources.Add(type, currentGoalRequirements[type] - _storage.GetAmount(type));
        }

        ResetRequirements(shouldBeSubscribed: true);
        TryEndGoal();
    }

    [Serializable]
    private class GoalRequirements
    {
        [SerializeField] private BaseGoals _goal;
        [SerializeField] private List<GoalRequirement> _resourceRequirements;

        public BaseGoals Goal => _goal;

        public Dictionary<ResourceType, int> GetRequiredAmount()
        {
            return _resourceRequirements.ToDictionary(requirement => requirement.Type, requirement => requirement.Amount);
        }
    }

    [Serializable]
    private class GoalRequirement
    {
        [SerializeField] private ResourceType _type;
        [SerializeField] private int _amount;

        public ResourceType Type => _type;
        public int Amount => _amount;
    }
}
