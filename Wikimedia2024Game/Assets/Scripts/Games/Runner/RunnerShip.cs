using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RunnerShip : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float[] positionByLine;
    [SerializeField] private int[] layerByLine;
    [SerializeField] private float changeLineSpeed = 1;
    [SerializeField] private Animator animator;
    public UnityEvent<GameObject> OnCrash;

    private float x;
    public int currentLine { get; private set; }

    private void Start()
    {
        currentLine = 1;
        x = transform.localPosition.x;
        transform.localPosition = new Vector3(x, positionByLine[currentLine], 0);
        spriteRenderer.sortingOrder = layerByLine[currentLine];
    }

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveUp();
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveDown();
            }
        }
    }

    private void MoveDown()
    {
        MoveToLine(Math.Min(positionByLine.Length - 1, currentLine + 1));
    }

    private void MoveUp()
    {
        MoveToLine(Math.Max(0, currentLine - 1));
    }

    private void MoveToLine(int otherLine)
    {
        StartCoroutine(AnimateMove(otherLine));
    }

    private bool isMoving;
    private IEnumerator AnimateMove(int otherLine)
    {
        if(otherLine > currentLine)
        {
            StartCoroutine(PegarSaltitoDeCambioDeCarril(otherLine));
        }

        isMoving = true;
        float dist = positionByLine[otherLine] - positionByLine[currentLine];
        float currentY = positionByLine[currentLine];

        while (Math.Abs(dist) > 0.1f)
        {
            var speed = Time.fixedDeltaTime * (dist > 0 ? 1 : -1) * changeLineSpeed;
            currentY = transform.localPosition.y + speed;
            transform.localPosition = new Vector3(x, currentY, 0);

            dist = positionByLine[otherLine] - currentY;

            yield return new WaitForEndOfFrame();
        }

        if (otherLine < currentLine)
        {
            StartCoroutine(PegarSaltitoDeCambioDeCarril(otherLine));
        }

        currentLine = otherLine;
        spriteRenderer.sortingOrder = layerByLine[currentLine];
        transform.localPosition = new Vector3(x, positionByLine[currentLine], 0);
        isMoving = false;
    }

    private IEnumerator PegarSaltitoDeCambioDeCarril(int otherLine)
    {
        int jumpIntensity = 2;
        int jumpSpeed = 2;
        var posY = transform.localPosition.y;
        for (int i = 0; i < jumpIntensity; i++)
        {
            var speed = Time.fixedDeltaTime * changeLineSpeed * jumpSpeed;
            posY = transform.localPosition.y + speed;
            transform.localPosition = new Vector3(x, posY, 0);
            yield return new WaitForEndOfFrame();
        }
        spriteRenderer.sortingOrder = layerByLine[otherLine];

        for (int i = 0; i < jumpIntensity; i++)
        {
            var speed = Time.fixedDeltaTime * changeLineSpeed * jumpSpeed;
            posY = transform.localPosition.y - speed;
            transform.localPosition = new Vector3(x, posY, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("saltar");
        OnCrash.Invoke(collision.gameObject);
    }
}
