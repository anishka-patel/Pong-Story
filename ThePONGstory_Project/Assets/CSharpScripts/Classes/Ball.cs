using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Ball: ScriptableObject
{
	public string ballName;
	public int hitPoint;
	public int xForce;
	public int yForce;
	public int minXSpeed;
	public int ballSelfModY;
	public int attack;
	public int defence;
	public int endurance;
	public List<Element> elementProperties;
	public GameObject theBall;

}