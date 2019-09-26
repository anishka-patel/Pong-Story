using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player: ScriptableObject
{
	public string playerName;
	public int experience;
	public int level;
	public Sprite ownedIcon;
	public List<Slider> ownedSliders;
	public List<Ball> ownwedBalls;
	public List<Element> ownedElements;

	public Player ()
	{}
}