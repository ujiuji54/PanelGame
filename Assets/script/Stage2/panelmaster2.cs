﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class panelmaster2 : MonoBehaviour
{
		
	public GameObject panel;
  public GameObject timer;
  public GameObject Cleartext;
  private Timer time;
	private bool nowturn;
	private GameObject[,] panels = new GameObject[3, 3];
	private bool[,] States = new bool[3, 3];
	private panel_kaiten1[,] pk = new panel_kaiten1[3, 3];
	Answer2 ans;


	int a, b;
	// Use this for initialization
	void Start ()
	{
		ans = gameObject.AddComponent<Answer2>();
    time = timer.GetComponent<Timer>();
		for (a = 0; a < 3; a++) {
			for (b = 0; b < 3; b++) {
				var aaa = Instantiate (panel);
				    
				aaa.transform.position = new Vector3 (a * 10 - 55, b * 10 - 30, 82);
				panels [a, b] = aaa;
				pk [a, b] = aaa.GetComponent<panel_kaiten1> ();
				pk [a, b].x = a;
				pk [a, b].y = b;
				States [a, b] = false;
				//Debug.Log (States [0, 0]);
			}
		}

	}

	public void turnpanels (int x, int y)
	{
		if (nowturn) {
			return;
		}
		pk [y, x].turning = true;
		if (y + 1 < 3)
			pk [y + 1, x].turning= true;
		if (y - 1 >= 0)
			pk [y - 1, x].turning = true;
		if (x + 1 < 3)
			pk [y, x + 1].turning = true;
		if (x - 1 >= 0)
			pk [y, x - 1].turning = true;

		States [y, x] = !States [y, x];
		if (y + 1 < 3)
			States [y + 1, x] = !States [y + 1, x];
		if (y - 1 >= 0)
			States [y - 1, x] = !States [y - 1, x];
		if (x + 1 < 3)
			States [y, x + 1] = !States [y, x + 1];
		if (x - 1 >= 0)
			States [y, x - 1] = !States [y, x - 1];
		Debug.Log (ans.check (States));
		if (ans.check (States)) {
      Cleartext.SetActive(true);
      time.timestop = true;
      Invoke("DelayMethod", 3.5f);
		}
	}

  void DelayMethod()
  {
    SceneManager.LoadScene("StageSelect");
  }

	// Update is called once per frame
	void Update ()
	{
		int x = 0;
		int y = 0;
		for (x = 0; x < 3; x++) {
			for (y = 0; y < 3; y++) {
				if (pk [y, x].turning) {
					nowturn = true;
					break;
				}
			}
			if (y != 3) {
				break;
			}
		}

		if (x == 3 && y == 3) {
			nowturn = false;
		}
	}
}