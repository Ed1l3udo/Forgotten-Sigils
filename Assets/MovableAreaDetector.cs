using UnityEngine;
using System.Collections.Generic;

public class MovableAreaDetector : MonoBehaviour
{
    private HashSet<Movable> movablesDentro = new HashSet<Movable>();

    public int detectionNumber;
    public int goal;
    public bool active;
    public int QuantidadeDeMovables => movablesDentro.Count;

    void OnTriggerEnter2D(Collider2D other)
    {
        Movable mov = other.GetComponent<Movable>();
        if (mov != null && mov.number == detectionNumber) movablesDentro.Add(mov);        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Movable mov = other.GetComponent<Movable>();

        if (mov != null && movablesDentro.Contains(mov)) movablesDentro.Remove(mov);
    }

    public bool ContemMovable(Movable mov)
    {
        return movablesDentro.Contains(mov);
    }

    public List<Movable> ObterMovables()
    {
        return new List<Movable>(movablesDentro);
    }

    void Start()
    {
        Collider2D col = GetComponent<Collider2D>();

        ContactFilter2D filtro = new ContactFilter2D();
        filtro.NoFilter();

        Collider2D[] resultados = new Collider2D[20];
        int contagem = col.Overlap(filtro, resultados);

        for (int i = 0; i < contagem; i++)
        {
            Movable mov = resultados[i].GetComponent<Movable>();
            if (mov != null && mov.number == detectionNumber) movablesDentro.Add(mov);
        }
    }

    void Update()
    {
        if (movablesDentro.Count == goal) active = true;
        else active = false;
    }
}
