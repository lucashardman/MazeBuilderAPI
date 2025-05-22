using MazeBuilderAPI.Interfaces;
using MazeBuilderAPI.Models.Enums;

namespace MazeBuilderAPI.Algorithms.Pathfinding;

using Models.Internal;
using Models.Responses;

public class DepthFirstSearchPathfinding : ISolveStrategy
{
    public PathfindingAlgorithm PathfindingAlgorithmName  => PathfindingAlgorithm.DepthFirstSearch;
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
        
        // Pilha para o DFS
        Stack<IntPoint> stack = new Stack<IntPoint>();
        
        // Inicializar
        stack.Push(start);
        visited[start.Y, start.X] = true;
        cameFrom[start] = null;
        
        bool foundPath = false;
        
        // Executar DFS
        while (stack.Count > 0)
        {
            IntPoint current = stack.Pop();
            
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
                
                // Verificar se há uma parede entre as células
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
                
                // Marcar como visitada e adicionar à pilha
                IntPoint next = new IntPoint(nextX, nextY);
                visited[nextY, nextX] = true;
                cameFrom[next] = current;
                stack.Push(next);
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