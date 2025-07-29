using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("Prefab do Monstro")]
    public GameObject monsterPrefab;

    [Header("Configuração da Área de Spawn")]
    public Vector2 areaSize = new Vector2(10f, 10f);

    [Header("Configuração de Spawn")]
    public int quantidadeInicial = 5;
    public float tempoMinEntreSpawns = 3f;
    public float tempoMaxEntreSpawns = 8f;
    public int maxMonstrosVivos = 10;
    public float distanciaMinima = 15f;

    private List<GameObject> monstrosVivos = new List<GameObject>();

    public GameObject player;

    private void Start()
    {
        for (int i = 0; i < quantidadeInicial; i++)
        {
            SpawnMonster();
        }

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float tempoEspera = Random.Range(tempoMinEntreSpawns, tempoMaxEntreSpawns);
            yield return new WaitForSeconds(tempoEspera);
            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        // Checa se o player está perto o suficiente do spawner
        float distanciaDoPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanciaDoPlayer > distanciaMinima) return;

        // Remove monstros mortos da lista
        monstrosVivos.RemoveAll(monster => monster == null);

        if (monstrosVivos.Count >= maxMonstrosVivos)
            return;

        // Tenta encontrar uma posição válida fora da câmera
        Vector2 spawnPosition;
        int tentativas = 10;
        bool posicaoValida = false;

        do
        {
            spawnPosition = (Vector2)transform.position + new Vector2(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                Random.Range(-areaSize.y / 2, areaSize.y / 2)
            );

            if (!IsPositionInsideCamera(spawnPosition))
            {
                posicaoValida = true;
                break;
            }

            tentativas--;
        } while (tentativas > 0);

        if (!posicaoValida) return;

        GameObject novoMonstro = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        monstrosVivos.Add(novoMonstro);
    }

    bool IsPositionInsideCamera(Vector2 position)
    {
        Camera cam = Camera.main;
        if (cam == null) return false;

        Vector3 viewportPoint = cam.WorldToViewportPoint(position);

        return viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaMinima);
    }
}
