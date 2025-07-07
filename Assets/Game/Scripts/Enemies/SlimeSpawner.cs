using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlimeSpawner : MonoBehaviour
{
    [Header("Prefab do Slime")]
    public GameObject slimePrefab;

    [Header("Configuração da Área de Spawn")]
    public Vector2 areaSize = new Vector2(10f, 10f);

    [Header("Configuração de Spawn")]
    public int quantidadeInicial = 5;
    public float tempoMinEntreSpawns = 3f;
    public float tempoMaxEntreSpawns = 8f;
    public int maxSlimesVivos = 10;

    private List<GameObject> slimesVivos = new List<GameObject>();

    public GameObject player;

    private void Start()
    {
        // Spawn inicial
        for (int i = 0; i < quantidadeInicial; i++)
        {
            SpawnSlime();
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
            SpawnSlime();
        }
    }

    void SpawnSlime()
    {
        // Remove slimes destruídos da lista
        slimesVivos.RemoveAll(slime => slime == null);

        if (slimesVivos.Count >= maxSlimesVivos)
            return;

        Vector2 spawnPosition = (Vector2)transform.position + new Vector2(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            Random.Range(-areaSize.y / 2, areaSize.y / 2)
        );

        GameObject novoSlime = Instantiate(slimePrefab, spawnPosition, Quaternion.identity);

        EnemyAI enemyAI = novoSlime.GetComponent<EnemyAI>();
        enemyAI.target = player.transform;

        slimesVivos.Add(novoSlime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}
