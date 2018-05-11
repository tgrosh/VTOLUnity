using SubjectNerd.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject {
    [Reorderable]
    public List<Message> messages = new List<Message>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
