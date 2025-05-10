using UnityEngine;
public abstract class MagicSpell : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;

    protected Vector2 direction;

    public void Initialize(Vector2 castDirection)
    {
        direction = castDirection;
        SetInitialState();
        Destroy(gameObject, lifeTime);
    }

    protected virtual void SetInitialState() {} //to put effects later

    protected virtual void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit(collision);
        Destroy(gameObject);
    }

    protected abstract void OnHit(Collider2D target); // effect when colliding

}
