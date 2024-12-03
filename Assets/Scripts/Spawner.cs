using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab; // O prefab que ser� instanciado.
        [Range(0f, 1f)] // O campo spawnChance s� pode aceitar valores entre 0 e 1
        public float spawnChance; // Chance de spawn deste objeto, entre 0 e 1.
    }

    public SpawnableObject[] objects; // Lista de objetos que podem ser instanciados.

    public float minSpawnRate = 1f; // Intervalo m�nimo entre os spawns.
    public float maxSpawnRate = 2f; // Intervalo m�ximo entre os spawns.

    private void OnEnable()
    {
        // Inicia o ciclo de spawn com um intervalo aleat�rio entre `minSpawnRate` e `maxSpawnRate`.
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        // Cancela qualquer invoca��o ativa para evitar spawns quando o GameObject for desativado.
        CancelInvoke();
    }

    private void Spawn()
    {
        // Gera um valor aleat�rio entre 0 e 1 para determinar qual objeto ser� instanciado.
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            // Verifica se o valor aleat�rio � menor que a chance de spawn do objeto atual.
            if (spawnChance < obj.spawnChance)
            {
                // Instancia o prefab e ajusta sua posi��o relativa ao spawner.
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break; // Sai do loop ap�s instanciar o objeto.
            }

            // Reduz o valor da chance restante para considerar o pr�ximo objeto.
            spawnChance -= obj.spawnChance;
        }

        // Reinvoca o m�todo Spawn ap�s um intervalo aleat�rio.
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
