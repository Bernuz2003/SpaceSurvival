using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    float punteggioTimer;
    float spawnTimer;
    float spawnRate = 1.5f;
    public GameObject ostacolo;
    public GameObject nemico1;
    public GameObject nemico2;
    public GameObject nemico3;
    public static bool gameover;
    public static bool creati;

    public static int ostacoli_passati;
    public static int nemiciUccisi;

    // Contatori specifici per ciascun tipo di nemico
    private int roundNemico1;
    private int roundNemico2;
    private int roundNemico3;

    private int num_nemici_iniziali_1;
    private int num_nemici_iniziali_2;
    private int num_nemici_iniziali_3;

    private bool bloccaOstacoli;

    // Start is called before the first frame update
    void Start()
    {
        ostacoli_passati = 0;
        nemiciUccisi = 0;
        creati = false;

        // Inizializzazione contatori per ciascun tipo di nemico
        roundNemico1 = 3;
        roundNemico2 = 2;
        roundNemico3 = 1;

        num_nemici_iniziali_1 = 1;
        num_nemici_iniziali_2 = 2;
        num_nemici_iniziali_3 = 1;

        bloccaOstacoli = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            if (!bloccaOstacoli)
            {
                generaOstacoli();
                if (ostacoli_passati >= 5)
                {
                    bloccaOstacoli = true;
                }
            }
            else
            {
                StartCoroutine(PausaPrimaDiEseguireSequenza());
            }

            punteggioTimer += Time.deltaTime;
            if (punteggioTimer > 0.1)
            {
                punteggioTimer = 0;
                Punti.valorePunti++;
            }
        }
    }

    IEnumerator PausaPrimaDiEseguireSequenza()
{
    yield return new WaitForSeconds(3f);  // Pausa di 3 secondi
    yield return StartCoroutine(EseguiSequenzaNemiciCoroutine());
    ostacoli_passati = 0;
    bloccaOstacoli = false;
    spawnTimer = 0f;  // Resetta il timer degli ostacoli quando inizia un nuovo round
}

IEnumerator EseguiSequenzaNemiciCoroutine()
{
    eseguiSequenzaNemici();
    // Attendi fino a quando la sequenza dei nemici è completata
    while (roundNemico1 > 0 || roundNemico2 > 0 || roundNemico3 > 0)
    {
        yield return null;
    }
}

    void eseguiSequenzaNemici()
    {
        if (roundNemico1 > 0)
        {
            eseguiRoundsNemici(nemico1, ref roundNemico1, ref num_nemici_iniziali_1, 2);
        }
        else if (roundNemico2 > 0)
        {
            eseguiRoundsNemici(nemico2, ref roundNemico2, ref num_nemici_iniziali_2, 1);
        }
        else if (roundNemico3 > 0)
        {
            eseguiRoundsNemici(nemico3, ref roundNemico3, ref num_nemici_iniziali_3, 0);
        }
    }
    
    void eseguiRoundsNemici(GameObject nemico, ref int quanti_round, ref int num_nemici, int incremento)
    {
        if (!creati)
        {
            generaOrda(nemico, num_nemici);
            creati = true;
        }

        if (nemiciUccisi == num_nemici)
        {
            // Se tutti i nemici del round sono stati uccisi, inizia un nuovo round
            num_nemici += incremento;  // Aumenta il numero di nemici per il prossimo round
            nemiciUccisi = 0;     // Reimposta il contatore di nemici uccisi
            creati = false;
            quanti_round--;
        }
    }
    
    void resettaValori() {
    	roundNemico1 = 3;
        roundNemico2 = 2;
        roundNemico3 = 1;

        num_nemici_iniziali_1 = 1;
        num_nemici_iniziali_2 = 2;
        num_nemici_iniziali_3 = 1;
    }

    void generaOstacoli()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            spawnTimer -= spawnRate;
            Vector2 spawnPosOstacolo = new Vector2(4.25f, Random.Range(-2f, 3f));
            Instantiate(ostacolo, spawnPosOstacolo, Quaternion.identity);
            ostacoli_passati++;
        }
    }

    void generaOrda(GameObject nemico, int n_nemici)
    {
        for (int i = 0; i < n_nemici; i++)
        {
            // Spawn dei nemici del nuovo round
            Vector2 spawnPosNemico = new Vector2(10f, Random.Range(-1.5f, 3.5f));
            Instantiate(nemico, spawnPosNemico, Quaternion.identity);
        }
    }
}

