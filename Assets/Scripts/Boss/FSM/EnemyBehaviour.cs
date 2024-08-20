using UnityEngine;
using System.Collections;

/// <summary>
/// Place the labels for the Transitions in this enum.
/// Don't change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public enum Transition
{
	NullTransition = 0, // Use this transition to represent a non-existing transition in your system
	SawPlayer = 1,
	LostPlayer = 2,
}

/// <summary>
/// Place the labels for the States in this enum.
/// Don't change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public enum StateID
{
	NullStateID = 0, // Use this ID to represent a non-existing State in your system
	PatrollingID = 1,
	ChasingID = 2,
}

public class EnemyBehaviour : MonoBehaviour {

	public FSMSystem fsm;
	private GameObject player;
	
	public float sightRange = 15.0f;

	public Transform[] wp;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		MakeFSM();
	}

	private void MakeFSM()
	{
        //TODO: Create two states (PatrolState and ChasingState), using the constructors defined below.
        //   Add one transition to each state (so each state can transit to the other), using the function FSMState.AddTransition
        //
        PatrolState patrolState = new PatrolState(gameObject, wp);
        ChasingState chasingState = new ChasingState(gameObject);
		patrolState.AddTransition(Transition.SawPlayer, StateID.ChasingID);
		chasingState.AddTransition(Transition.LostPlayer, StateID.PatrollingID);
        //

        fsm = new FSMSystem();
		// Add both states to the FSM at the end of this method
		//
		fsm.AddState(patrolState);
		fsm.AddState(chasingState);
		//
	}

	public void SetTransition(Transition t) { fsm.PerformTransition(t); }

	// Update is called once per frame
	void Update () {
		fsm.CurrentState.Reason(player, gameObject);
		fsm.CurrentState.Act(player, gameObject);
	}

	
	public bool PlayerInSight(GameObject player, GameObject npc)
	{
		float toPlayer = Vector2.Distance(player.transform.position, npc.transform.position);
		if (toPlayer <= sightRange)
			return true;
		return false;
	}
}


public class PatrolState : FSMState
{
	private EnemyAnimation enemyAnimation;
	private float patrolSpeed = 4f;

	private int currentWayPoint;
	private Transform[] waypoints;

	public PatrolState(GameObject thisObject, Transform[] wp) 
	{
		waypoints = wp;
		currentWayPoint = 0;
		stateID = StateID.PatrollingID;
		enemyAnimation = thisObject.GetComponent<EnemyAnimation>();
	}
	
	public override void Reason(GameObject player, GameObject npc)
	{
		//Check line of sight.
		if (npc.GetComponent<EnemyBehaviour>().PlayerInSight (player, npc)) {
            //TODO: Make a transition using Transition.SawPlayer.
            //
			npc.GetComponent<EnemyBehaviour>().SetTransition(Transition.SawPlayer);
			//
		}
	}

	public override void Act(GameObject player, GameObject npc)
	{
		// Program the Patrol State Act. It should update the currentWayPoint to the next one, 
		// in case the current one is reached by the agent.

		float toWaypoint = Vector3.Distance(waypoints[currentWayPoint].position, npc.transform.position);
		if (toWaypoint < 2f)
		{
			if (currentWayPoint == waypoints.Length - 1)
				currentWayPoint = 0;
			else
				currentWayPoint++;
		}
		//Update the target.
		enemyAnimation.SetTarget(waypoints[currentWayPoint], patrolSpeed);
	}	
}


public class ChasingState : FSMState
{
	private EnemyAnimation enemyAnimation;
	private float chasingSpeed = 8.0f;
	private float stopDist = 2f;

	public ChasingState(GameObject thisObject) 
	{ 
		stateID = StateID.ChasingID;
		enemyAnimation = thisObject.GetComponent<EnemyAnimation> ();
	}
	
	public override void Reason(GameObject player, GameObject npc)
	{
		if (!npc.GetComponent<EnemyBehaviour>().PlayerInSight (player, npc)) {
			//TODO: Make a transition using Transition.LostPlayer.
			//
			npc.GetComponent<EnemyBehaviour>().SetTransition(Transition.LostPlayer);
			//
		}
	}
	
	public override void Act(GameObject player, GameObject npc)
	{
		float toPlayer = npc.transform.position.x - player.transform.position.x;
		if (Mathf.Abs(toPlayer) < stopDist)
		{
			enemyAnimation.Stop();
			return;
		}
		enemyAnimation.SetTarget(player.transform, chasingSpeed);
	}	
} 



