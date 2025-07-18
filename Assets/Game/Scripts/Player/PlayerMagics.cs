using UnityEngine;
using System.Collections.Generic;

// Script pra lidar com todas as magias do player
// Tem a iceBlast comentada como um exemplo de como adicionar mais magias nesse script posteriormente
// A forma como isso aqui funciona é com um "cycle" de magias, ou seja, apertamos alguma tecla pra ciclar entre magias 
// e todas elas são chamadas no mouse esquerdo -> isso pode mudar no futuro
public class PlayerMagics : MonoBehaviour
{
    public GameObject fireballPrefab;  // Referência ao prefab da Fireball
    public GameObject forceballPrefab;
    // public GameObject iceBlastPrefab;  // Referência ao prefab de uma outra magia
    
    private  List<BaseMagic> availableMagics;  // Lista para armazenar magias

    private int currentMagicIndex; // Índice da magia atual equipada

    private PlayerMana playerMana; // Referenciando o script PlayerMana pra pegar a mana 

    void Start()
    {

        playerMana = GetComponent<PlayerMana>();
        // Inicializando o dicionário com as magias disponíveis
        availableMagics = new List<BaseMagic>();

        // Adicionando magias ao dicionário
        availableMagics.Add(new FireBall(fireballPrefab, 1));
        availableMagics.Add(new Force(forceballPrefab, 1));
        // availableMagics.Add(new IceBlast(iceBlastPrefab), 1); // Exemplo de outra magia

        // Definindo o índice de magias -> a magia indexada com o zero é a padrão ao iniciar o jogo
        currentMagicIndex = 0;
    }

    void Update()
    {
        // Se o jogador apertar o botão esquerdo do mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Obtemos a posição do mouse no mundo
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;  // Garantir que a magia fique no plano 2D

            // Chamamos a magia atual se tiver mana suficiente
            if(playerMana.TemMana(availableMagics[currentMagicIndex].ManaCost)){

                playerMana.UseMana(availableMagics[currentMagicIndex].ManaCost);
                availableMagics[currentMagicIndex].Cast(transform, mouseWorldPosition);

            }
            else{
                Debug.Log("Mana insuficiente!");
            }
        }

        // Trocar de magia com as teclas "Q" e "E"
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Cicla pra próxima magia
            currentMagicIndex = (currentMagicIndex + 1) % availableMagics.Count;
            Debug.Log("Magia atual: ");
            Debug.Log(availableMagics[currentMagicIndex]);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Cicla pra magia anterior
            currentMagicIndex = (currentMagicIndex - 1) % availableMagics.Count;
            if(currentMagicIndex < 0) currentMagicIndex = availableMagics.Count - 1;
            Debug.Log("Magia atual: ");
            Debug.Log(availableMagics[currentMagicIndex]);
        }
    }
}
