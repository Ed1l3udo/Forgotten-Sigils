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

    private Vector2 areaMinima;

    private List<GameObject> monstrosVivos = new List<GameObject>();

    public GameObject player;

    private void Start()
    {
        // Spawn inicial
        for (int i = 0; i < quantidadeInicial; i++)
        {
            SpawnMonster();
        }

        // Inicia spawn contínuo com tempo aleatório
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
        float distanciaDoPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        if (distanciaDoPlayer > distanciaMinima) return;

        monstrosVivos.RemoveAll(monster => monster == null);

        if (monstrosVivos.Count >= maxMonstrosVivos)
            return;

        Vector2 spawnPosition = (Vector2)transform.position + new Vector2(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            Random.Range(-areaSize.y / 2, areaSize.y / 2)
        );

        GameObject novoMonstro = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

        if(novoMonstro.GetComponent<EnemyAI>() != null){
            EnemyAI enemyAI = novoMonstro.GetComponent<EnemyAI>();
            enemyAI.target = player.transform;
        }

        monstrosVivos.Add(novoMonstro);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(distanciaMinima, distanciaMinima, 0f));
    }

}
