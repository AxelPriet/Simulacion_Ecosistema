using UnityEngine;
using System.Collections.Generic;

public class Ecosistema : MonoBehaviour
{
    // Parámetros iniciales
    public int plantasInicial = 200;
    public int herbivorosInicial = 40;
    public int carnivorosInicial = 8;

    public float tasaCrecimientoPlantas = 0.3f;
    public float consumoHerbivoros = 0.05f;
    public float eficienciaHerbivoros = 0.02f;
    public float mortalidadHerbivoros = 0.1f;
    public float consumoCarnivoros = 0.02f;
    public float eficienciaCarnivoros = 0.01f;
    public float mortalidadCarnivoros = 0.1f;

    private int plantas, herbivoros, carnivoros;

    // Prefabs
    public GameObject plantaPrefab;
    public GameObject herbivoroPrefab;
    public GameObject carnivoroPrefab;

    // Para manejar los objetos en escena
    private List<GameObject> plantasObjs = new List<GameObject>();
    private List<GameObject> herbivorosObjs = new List<GameObject>();
    private List<GameObject> carnivorosObjs = new List<GameObject>();

    // Timer
    private float timer = 0f;
    private float secondsPerIteration = 1f;

    void Start()
    {
        plantas = plantasInicial;
        herbivoros = herbivorosInicial;
        carnivoros = carnivorosInicial;

        Draw();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= secondsPerIteration)
        {
            timer = 0;
            Simulate();
        }
    }

    void Simulate()
    {
        // Fórmulas del ecosistema
        //Formula de crecimiento de las plantas
        int nuevasPlantas = Mathf.RoundToInt(plantas + tasaCrecimientoPlantas * plantas - consumoHerbivoros * herbivoros);
        //Formula de crecimiento de los herviboros
        int nuevosHerbivoros = Mathf.RoundToInt(herbivoros + eficienciaHerbivoros * consumoHerbivoros * herbivoros - mortalidadHerbivoros * herbivoros - consumoCarnivoros * carnivoros);
        //Formula de crecimiento de los Carnivoros
        int nuevosCarnivoros = Mathf.RoundToInt(carnivoros + eficienciaCarnivoros * consumoCarnivoros * carnivoros - mortalidadCarnivoros * carnivoros);

        plantas = Mathf.Max(0, nuevasPlantas);
        herbivoros = Mathf.Max(0, nuevosHerbivoros);
        carnivoros = Mathf.Max(0, nuevosCarnivoros);

        Debug.Log($"Plantas: {plantas}, Herbívoros: {herbivoros}, Carnívoros: {carnivoros}");

        Draw();
    }

    void Draw()
    {
        // Limpiar escena
        foreach (var obj in plantasObjs) Destroy(obj);
        foreach (var obj in herbivorosObjs) Destroy(obj);
        foreach (var obj in carnivorosObjs) Destroy(obj);
        plantasObjs.Clear();
        herbivorosObjs.Clear();
        carnivorosObjs.Clear();

        // Instanciar plantas
        for (int i = 0; i < plantas; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0);
            GameObject p = Instantiate(plantaPrefab, pos, Quaternion.identity);
            plantasObjs.Add(p);
        }

        // Instanciar herbívoros
        for (int i = 0; i < herbivoros; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0);
            GameObject h = Instantiate(herbivoroPrefab, pos, Quaternion.identity);
            herbivorosObjs.Add(h);
        }

        // Instanciar carnívoros
        for (int i = 0; i < carnivoros; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0);
            GameObject c = Instantiate(carnivoroPrefab, pos, Quaternion.identity);
            carnivorosObjs.Add(c);
        }
    }
}

