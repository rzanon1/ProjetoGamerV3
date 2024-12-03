using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab; // O prefab que será instanciado.
        [Range(0f, 1f)] // O campo spawnChance só pode aceitar valores entre 0 e 1
        public float spawnChance; // Chance de spawn deste objeto, entre 0 e 1.
    }

    public SpawnableObject[] objects; // Lista de objetos que podem ser instanciados.

    public float minSpawnRate = 1f; // Intervalo mínimo entre os spawns.
    public float maxSpawnRate = 2f; // Intervalo máximo entre os spawns.

    private void OnEnable()
    {
        // Inicia o ciclo de spawn com um intervalo aleatório entre `minSpawnRate` e `maxSpawnRate`.
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        // Cancela qualquer invocação ativa para evitar spawns quando o GameObject for desativado.
        CancelInvoke();
    }

    private void Spawn()
    {
        // Gera um valor aleatório entre 0 e 1 para determinar qual objeto será instanciado.
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            // Verifica se o valor aleatório é menor que a chance de spawn do objeto atual.
            if (spawnChance < obj.spawnChance)
            {
                // Instancia o prefab e ajusta sua posição relativa ao spawner.
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break; // Sai do loop após instanciar o objeto.
            }

            // Reduz o valor da chance restante para considerar o próximo objeto.
            spawnChance -= obj.spawnChance;
        }

        // Reinvoca o método Spawn após um intervalo aleatório.
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
