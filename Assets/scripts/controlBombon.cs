﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class controlBombon : MonoBehaviour {


	private bool JumpFlag = false;
	public bool Error=false, BienRespondido=false;
	private float Tiempoerror=0.2f, tiempoinmune=0f;
	private int dir=2;
	private Animator animator;
	private int puntos=0;
	public Text puntostext;
	private float TiempoparaIDLE =0.2f;
	void Start () {

		animator = GetComponent<Animator> ();


	}
	// Update is called once per frame
	void Update () {
		
		Tiempoerror -= Time.deltaTime;
		tiempoinmune -= Time.deltaTime;
		if (Tiempoerror <= 0) {
			Error = false;
		}

		float moveInput = Input.GetAxis("Horizontal") * Time.deltaTime * 4;
	
		if (moveInput == 0) {
			if (TiempoparaIDLE<=0) {
				animator.SetBool ("Corriendo", false);
				TiempoparaIDLE = 0.2f;
			}
			TiempoparaIDLE -= Time.deltaTime;

		}else if (moveInput < 0) {
			dir = -2;

			animator.SetBool ("Corriendo", true);
		} else if(moveInput > 0) {
			dir = 2;
			animator.SetBool ("Corriendo", true);
		}
		transform.localScale = new Vector3 (dir, 2, 0);
		transform.position += new Vector3 (moveInput, 0, 0);

		if (Input.GetButtonDown ("Jump") && JumpFlag) {
			animator.SetTrigger ("Saltar");
			GetComponent<Rigidbody2D>().AddForce(new Vector3(0,300,0));

		} 


	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Piso") {
			JumpFlag = true;
			animator.SetBool ("Piso", true);
		}
		if (col.gameObject.tag == "BuenaPalabra" && tiempoinmune<=0) {
			if(puntos<14){
			puntos += 1;
			puntostext.text = puntos.ToString ();
			}else {
					puntostext.text = "You Win";
				}
			tiempoinmune = 1f;

			BienRespondido = true;
		}
		if (col.gameObject.tag == "MalaPalabra" && tiempoinmune<=0) {
			Error = true;
			Tiempoerror = 0.2f;
			tiempoinmune = 1f;


			//Guardar la poscición de la pregunta para lanzarla después
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag == "Piso") {
			JumpFlag = false;
			animator.SetBool ("Piso", false);

		}

	}


}
