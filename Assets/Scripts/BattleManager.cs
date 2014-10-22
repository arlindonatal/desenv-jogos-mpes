using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public GameObject[] EnemySpawnPoints;
    public GameObject[] EnemyPrefabs;
    public AnimationCurve SpawnAnimationCurve;
    private Animator battleStateManager;

    private int enemyCount;
    private Dictionary<int, BattleState> battleStateHash = new Dictionary<int, BattleState>();
    private BattleState currentBattleState;

    private InventoryItem selectedWeapon;
    private string selectedTargetName;
    private EnemyController selectedTarget;
    public GameObject selectionCircle;

    private bool canSelectEnemy;

    bool attacking = false;

    public bool CanSelectEnemy
    {
        get
        {
            return canSelectEnemy;
        }
    }

    public int EnemyCount
    {
        get
        {
            return enemyCount;
        }
    }

    public enum BattleState
    {
        Begin_Battle,
        Intro,
        Player_Move,
        Player_Attack,
        Change_Control,
        Enemy_Attack,
        Battle_Result,
        Battle_End
    }

    // Use this for initialization
    void Start()
    {
        battleStateManager = GetComponent<Animator>();

        //debug
        GameState.currentPlayer.Health = 10;

        MessagingManager.Instance.SubscribeInventoryEvent(InventoryItemSelect);
        MessagingManager.Instance.SubscribeUIEvent((state) => canSelectEnemy = false);

        GetAnimationStates();

        // Calculate how many enemies
        enemyCount = Random.Range(1, EnemySpawnPoints.Length);
        // Spawn the enemies in
        StartCoroutine("SpawnEnemies");

    }

    void GetAnimationStates()
    {
        foreach (BattleState state in (BattleState[])System.Enum.GetValues(typeof(BattleState)))
        {
            battleStateHash.Add(Animator.StringToHash("Base Layer." + state.ToString()), state);
        }
    }

    IEnumerator SpawnEnemies()
    {
        // Spawn enemies in over time
        for (int i = 0; i < enemyCount; i++)
        {
            var newEnemy = (GameObject)Instantiate(EnemyPrefabs[0]);
            newEnemy.transform.position = new Vector3(10, -1, 0);
            yield return StartCoroutine(MoveCharacterToPoint(EnemySpawnPoints[i], newEnemy));
            newEnemy.transform.parent = EnemySpawnPoints[i].transform;

            var controller = newEnemy.GetComponent<EnemyController>();
            controller.BattleManager = this;

            var EnemyProfile = ScriptableObject.CreateInstance<Enemy>();
            EnemyProfile.Class = EnemyClass.Goblin;
            EnemyProfile.Level = 1;
            EnemyProfile.Damage = 1;
            EnemyProfile.Health = 2;
            EnemyProfile.Name = EnemyProfile.Class + " " + i.ToString();
            controller.EnemyProfile = EnemyProfile;
        }
        battleStateManager.SetBool("BattleReady", true);
    }



    IEnumerator MoveCharacterToPoint(GameObject destination, GameObject character)
    {
        float timer = 0f;
        var StartPosition = character.transform.position;
        while (destination.transform.position != character.transform.position)
        {
            character.transform.position = Vector3.Lerp(StartPosition, destination.transform.position, SpawnAnimationCurve.Evaluate(timer));
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    public void SelectEnemy(EnemyController enemy, string name)
    {
        selectedTarget = enemy;
        selectedTargetName = name;
    }

    public void ClearSelectedEnemy()
    {
        if (selectedTarget != null)
        {
            var enemyController = selectedTarget.GetComponent<EnemyController>();
            enemyController.ClearSelection();
            selectedTarget = null;
            selectedTargetName = string.Empty;
        }
    }

    private void InventoryItemSelect(InventoryItem item)
    {
        selectedWeapon = item;
        if (item == null)
        {
            ClearSelectedEnemy();
        }
    }

    IEnumerator AttackTarget()
    {
        int Attacks = 0;
        attacking = true;
        bool attackComplete = false;
        while (!attackComplete)
        {
            GameState.currentPlayer.Attack(selectedTarget.EnemyProfile);
            selectedTarget.UpdateAI();
            Attacks++;
            if (selectedTarget.EnemyProfile.Health < 1 || Attacks > GameState.currentPlayer.NoOfAttacks)
            {
                attackComplete = true;
            }
            yield return new WaitForSeconds(1);
        }
    }

    void FixedUpdate()
    {
        if (currentBattleState == BattleState.Battle_End && Input.anyKey)
        {
            NavigationManager.GoBack();
        }
    }
    void Update()
    {
        currentBattleState = battleStateHash[battleStateManager.GetCurrentAnimatorStateInfo(0).nameHash];
        switch (currentBattleState)
        {
            case BattleState.Intro:
                break;
            case BattleState.Player_Move:
                break;
            case BattleState.Player_Attack:
                if (!attacking)
                {
                    StartCoroutine(AttackTarget());
                }
                break;
            case BattleState.Change_Control:
                break;
            case BattleState.Enemy_Attack:
                break;
            case BattleState.Battle_Result:
                break;
            case BattleState.Battle_End:
                NavigationManager.GoBack();
                break;
            default:
                break;
        }
    }

    void OnGUI()
    {
        switch (currentBattleState)
        {
            case BattleState.Begin_Battle:
                break;
            case BattleState.Intro:
                GUI.Box(new Rect((Screen.width / 2) - 150, 150, 300, 50), "Battle between Player and Goblins");
                break;
            case BattleState.Player_Move:
                if (GUI.Button(new Rect(10, 10, 100, 50), "Run Away"))
                {
                    GameState.playerReturningHome = true;
                    NavigationManager.NavigateTo("World");
                }
                if (selectedWeapon == null)
                {
                    canSelectEnemy = false;
                    GUI.Box(new Rect((Screen.width / 2) - 50, 100, 100, 50), "Select Weapon");
                }
                else if (selectedTarget == null)
                {
                    GUI.Box(new Rect((Screen.width / 2) - 50, 100, 100, 50), "Select Target");
                    canSelectEnemy = true;
                }
                else
                {
                    if (GUI.Button(new Rect((Screen.width / 2) - 50, 100, 100, 50), "Attack " + selectedTargetName))
                    {
                        battleStateManager.SetBool("PlayerReady", true);
                        MessagingManager.Instance.BroadcastUIEvent(true);
                        canSelectEnemy = false;
                    }
                }
                break;
            case BattleState.Player_Attack:
                break;
            case BattleState.Change_Control:
                break;
            case BattleState.Enemy_Attack:
                break;
            case BattleState.Battle_Result:
                break;
            default:
                break;
        }
    }
}
