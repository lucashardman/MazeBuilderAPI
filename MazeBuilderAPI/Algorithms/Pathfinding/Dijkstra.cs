using MazeBuilderAPI.Interfaces;
using MazeBuilderAPI.Models.Enums;

namespace MazeBuilderAPI.Algorithms.Pathfinding;

using Models.Internal;
using Models.Responses;
using System.Collections.Generic;

public class Dijkstra  : ISolveStrategy
{
    public PathfindingAlgorithm PathfindingAlgorithmName => PathfindingAlgorithm.Dijkstra;

    public SolveResponse Solve(List<List<MazeVertex>> maze)
    {
        int height = maze.Count;
        int width = maze[0].Count;

        // Ponto inicial (0,0) e ponto final (width-1, height-1)
        IntPoint start = new IntPoint(0, 0);
        IntPoint end = new IntPoint(width - 1, height - 1);

        // Matriz para marcar células visitadas
        bool[,] visited = new bool[height, width];

        // Dicionário para armazenar o caminho
        Dictionary<IntPoint, IntPoint?> cameFrom = new Dictionary<IntPoint, IntPoint?>();

        // Dicionário para armazenar as distâncias
        Dictionary<IntPoint, int> distances = new Dictionary<IntPoint, int>();

        // Inicializar distâncias com "infinito"
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                distances[new IntPoint(x, y)] = int.MaxValue;
            }
        }

        // Fila de prioridade para o Dijkstra (usando uma lista ordenada por distância)
        List<(IntPoint point, int distance)> priorityQueue = new List<(IntPoint, int)>();

        // Inicializar
        distances[start] = 0;
        priorityQueue.Add((start, 0));
        cameFrom[start] = null;

        bool foundPath = false;

        // Executar Dijkstra
        while (priorityQueue.Count > 0)
        {
            // Encontrar o nó com menor distância
            priorityQueue.Sort((a, b) => a.distance.CompareTo(b.distance));
            var (current, currentDistance) = priorityQueue[0];
            priorityQueue.RemoveAt(0);

            // Se já visitamos este nó, pular
            if (visited[current.Y, current.X])
                continue;

            // Marcar como visitado
            visited[current.Y, current.X] = true;

            // Se chegamos ao destino, terminamos
            if (current.X == end.X && current.Y == end.Y)
            {
                foundPath = true;
                break;
            }

            // Direções possíveis: cima, direita, baixo, esquerda
            var directions = new List<(int dx, int dy)>
            {
                (0, -1), // Cima
                (1, 0),  // Direita
                (0, 1),  // Baixo
                (-1, 0)  // Esquerda
            };

            // Verificar todas as direções possíveis
            foreach (var (dx, dy) in directions)
            {
                int nextX = current.X + dx;
                int nextY = current.Y + dy;

                // Verificar se a próxima célula está dentro dos limites
                if (nextX < 0 || nextX >= width || nextY < 0 || nextY >= height)
                    continue;

                // Verificar se já visitamos esta célula
                if (visited[nextY, nextX])
                    continue;

                // Verificar se há uma passagem entre as células
                bool canMove = false;

                if (dx == 0 && dy == -1) // Cima
                    canMove = maze[current.Y][current.X].UpEdge;
                else if (dx == 1 && dy == 0) // Direita
                    canMove = maze[current.Y][current.X].RightEdge;
                else if (dx == 0 && dy == 1) // Baixo
                    canMove = maze[current.Y][current.X].DownEdge;
                else if (dx == -1 && dy == 0) // Esquerda
                    canMove = maze[current.Y][current.X].LeftEdge;

                if (!canMove)
                    continue;

                // Calcular nova distância (assumindo peso 1 para cada movimento)
                IntPoint next = new IntPoint(nextX, nextY);
                int newDistance = distances[current] + 1;

                // Se encontramos um caminho mais curto para este nó
                if (newDistance < distances[next])
                {
                    distances[next] = newDistance;
                    cameFrom[next] = current;
                    priorityQueue.Add((next, newDistance));
                }
            }
        }

        // Se não encontramos um caminho, retornar lista vazia
        if (!foundPath)
        {
            return new SolveResponse { Path = new List<IntPoint>(), PathLength = 0 };
        }

        // Reconstruir o caminho
        List<IntPoint> path = new List<IntPoint>();
        IntPoint? currentVertex = end;

        while (currentVertex != null)
        {
            path.Add(currentVertex.Value);

            if (currentVertex.Value.X == start.X && currentVertex.Value.Y == start.Y)
                break;

            if (!cameFrom.ContainsKey(currentVertex.Value))
                break; // Proteção contra loops infinitos

            currentVertex = cameFrom[currentVertex.Value];
        }

        // Inverter o caminho para ser do início até o fim
        path.Reverse();

        return new SolveResponse
        {
            Path = path,
            PathLength = path.Count > 0 ? path.Count - 1 : 0
        };
    }
}