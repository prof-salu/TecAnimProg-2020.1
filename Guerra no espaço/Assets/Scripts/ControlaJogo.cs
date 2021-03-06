﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogo : MonoBehaviour
{
    public GameObject jogadorPrefab;
    private GameObject jogador;
    public int vidas;
    private float timerReaparecer;
    public static int placar;

    void Start()
    {
        placar = 0;
        vidas = 3;
        NovaVida();
    }

    // Update is called once per frame
    void Update()
    {
        if(jogador == null && vidas > 0)
        {
            timerReaparecer -= Time.deltaTime;

            if(timerReaparecer < 0)
            {
                NovaVida();
            }
        }

        if(vidas <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("fase01");
            }
        }
    }

    private void NovaVida()
    {
        timerReaparecer = 1;
        jogador = (GameObject) Instantiate(jogadorPrefab, transform.position, transform.rotation);
        StartCoroutine(Invencibilidade());
        vidas--;        
    }

    private IEnumerator Invencibilidade()
    {
        //Vetor ou array
        BoxCollider2D[] colisores = jogador.GetComponents<BoxCollider2D>();

        //rotinas
        //Como o GameObject possui mais de um colisor, devemos desabilitar todos os colisores da Nave
        foreach(BoxCollider2D colisor in colisores)
        {
            colisor.enabled = false;
        }
        yield return new WaitForSeconds(2f);
        //Como o GameObject possui mais de um colisor, devemos habilitar todos os colisores da Nave
        foreach (BoxCollider2D colisor in colisores)
        {
            colisor.enabled = true;
        }
    }

    private void OnGUI()
    {
        if(vidas > 0 || jogador != null)
        {            
            if (jogador != null)
            {   
                //Quando a nave do jogador estiver ativa, exibe os valores atuais de cada propriedade
                GUI.Label(new Rect(0, 40, 200, 50), "Atraso no disparo: " + jogador.GetComponent<ControlaDisparo>().atrasoNoDisparo);
                GUI.Label(new Rect(0, 60, 200, 50), "Recarga Escudo: " + jogador.GetComponent<AtivaEscudo>().timerRecargaEscudo);
                GUI.Label(new Rect(0, 80, 200, 50), "Velocidade Nave: " + jogador.GetComponent<ControlaJogador>().velocidadeMax);
            }else
            {
                //Quando a nave do jogador não está na cena exibe os valores zerados
                GUI.Label(new Rect(0, 40, 200, 50), "Atraso no disparo: 0");
                GUI.Label(new Rect(0, 60, 200, 50), "Recarga Escudo: 0");
                GUI.Label(new Rect(0, 80, 200, 50), "Velocidade Nave: 0");
            }
        }
        else
        {
            GUI.Label(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 25, 150, 50), "Game Over\nAperte R para recomeçar");
        }

        //new Rect ==> posX, posY, larguradaCaixa, AlturaDaCaixa
        GUI.Label(new Rect(0, 0, 150, 50), "Vidas restantes: " + vidas);
        GUI.Label(new Rect(0, 20, 150, 50), "Placar: " + placar);
    }
}