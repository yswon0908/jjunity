using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum playerstate
{
    IDLE = 0,
    RUN,
    CHASE,
    ATTACK
}


public class FSMManager : MonoBehaviour {

    public playerstate currentState;
    public playerstate startState;
    public Transform marker;

    Dictionary<playerstate, PlayerFSMState> states = new Dictionary<playerstate, PlayerFSMState>();

    private void Awake()
    {
        marker = GameObject.FindGameObjectWithTag("Marker").transform;

        states.Add(playerstate.IDLE, GetComponent<PlayerIDEL>());
        states.Add(playerstate.RUN, GetComponent<PlayerRUN>());
        states.Add(playerstate.CHASE, GetComponent<PlayerCHASE>());
        states.Add(playerstate.ATTACK, GetComponent<PlayerATTACK>());
    }

    public void SetState(playerstate newState)
    {
        foreach(PlayerFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }
        states[playerstate.IDLE].enabled = false;
        states[playerstate.RUN].enabled = false;
        states[playerstate.CHASE].enabled = false;
        states[playerstate.ATTACK].enabled = false;

        states[newState].enabled = true;
    }
    // Use this for initialization
    void Start ()
    {
        SetState(startState);
    }
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(r, out hit, 1000))
            {
                marker.position = hit.point;

                SetState(playerstate.RUN);

                




            }
        }
	}
}
