using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class CooldownManager{
	/*
		CooldownManager cooldownManager = new CooldownManager ();
	*/

	public static float oneSecond = 1f;
	private float time;
	private List<Cooldown> list;

	private class Cooldown{
		public string name;
		public float startTime;
		public float duration;

		public Cooldown(string n, float s, float d){
			name = n;
			startTime = s;
			duration = d;
		}
	}

	public CooldownManager(){
		time = 0f;
		list = new List<Cooldown>();
	}

	public void addDeltaTime(float deltaTime){
		time += deltaTime;
	}

	public float getTime(){
		return time;
	}

	public bool startCooldown(string name, float duration){
		int pos = findCooldownPosition (name);
		if (pos == -1) {
			list.Add (new Cooldown (name, time, duration));
			return true;
		} else {
			list[pos] = new Cooldown (name, time, duration);
			return true;
		}
	}

	public bool endCooldown(string name){
		Cooldown cd = findCooldown (name);
		if (cd != null) {
			cd.duration = 0f;
			return true;
		}
		return false;
	}

	public float timeOnCooldown(string name){
		Cooldown cd = findCooldown (name);
		if(cd!=null){
			return time - cd.startTime;
		}
		return 0f;
	}

	public bool cooldownExist(string name){
		Cooldown cd = findCooldown (name);
		if(cd!=null) return true;
		return false;
	}

	public bool isOnCooldown(string name){
		Cooldown cd = findCooldown (name);
		if(cd!=null){
			if (time < cd.startTime+cd.duration) return true;
		}
		return false;
	}

	public bool isNotOnCooldown(string name){
		return !isOnCooldown(name);
	}

	private int findCooldownPosition(string name){
		for (int i = 0; i < list.Count; i++) {
			if(list[i].name==name){
				return i;
			}
		}
		return -1;
	}

	private Cooldown findCooldown(string name){
		for (int i = 0; i < list.Count; i++) {
			if(list[i].name==name){
				return list [i];
			}
		}
		return null;
	}

}



