using UnityEngine;

[RequireComponent(typeof(StorageChecker))]
[RequireComponent(typeof(ObjectSpawner))]
[RequireComponent(typeof(BotBaseBuilder))]

public class GoalSetter : MonoBehaviour, IUsable
{
    private readonly int MinBotAmountForBuildingBase = 2;

    private StorageChecker _storageChecker;
    private ObjectSpawner _botSpawner;
    private BotBaseBuilder _baseBuilder;

    private InputReader _input;

    private void Awake()
    {
        _storageChecker = GetComponent<StorageChecker>();
        _botSpawner = GetComponent<ObjectSpawner>();
        _baseBuilder = GetComponent<BotBaseBuilder>();
    }

    private void OnEnable()
    {
        _storageChecker.GoalDone += DoActionBasedOnGoal;
        _botSpawner.ReceivedObject += ChangeGoalOnReceivedBot;
    }

    private void OnDisable()
    {
        _storageChecker.GoalDone -= DoActionBasedOnGoal;
        _botSpawner.ReceivedObject -= ChangeGoalOnReceivedBot;
        
        if (_input != null)
            _input.UsableHit -= StartGoalBuildBase;
    }

    private void Start()
    {
        ChangeGoal();
    }

    public void OnUse(InputReader input)
    {
        _input = input;
        _input.UsableHit += StartGoalBuildBase;
    }

    private void StartGoalBuildBase(RaycastHit usable)
    {
        _input.UsableHit -= StartGoalBuildBase;
        
        if (usable.collider.TryGetComponent(out Ground _) && _botSpawner.ActiveObjectsAmount >= MinBotAmountForBuildingBase)
        {
            _baseBuilder.SetBasePosition(usable.point);

            if (_storageChecker.CurrentGoal != BaseGoals.BuildBase)
                _storageChecker.SetGoal(BaseGoals.BuildBase);
        }
    }

    private void ChangeGoalOnReceivedBot()
    {
        if (_storageChecker.CurrentGoal != BaseGoals.BuildBase)
            ChangeGoal();
    }

    private void ChangeGoal()
    {
        if (_botSpawner.WillDestroyNewObject == false)
        {
            _storageChecker.SetGoal(BaseGoals.MakeBot);
        }
        else
        {
            if (_storageChecker.CheckIfStorageIsFull() == false)
            {
                _storageChecker.SetGoal(BaseGoals.FillStorage);
            }
            else
            {
                _storageChecker.SetGoal(BaseGoals.None);
            }
        }
    }

    private void DoActionBasedOnGoal(BaseGoals achievedGoal)
    {
        switch (achievedGoal)
        {
            case BaseGoals.None:

                break;

            case BaseGoals.FillStorage:

                break;


            case BaseGoals.MakeBot:
                _botSpawner.SpawnObject();

                break;


            case BaseGoals.BuildBase:
                _baseBuilder.SendBotToBuildBase();

                break;
        }

        ChangeGoal();
    }
}
