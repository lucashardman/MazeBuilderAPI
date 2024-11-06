using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Algorithms.Maze;

public class MazeBuilderBaseAlgorithm
{
    protected List<List<MazeVertex>>? Maze { get; set; }

    protected bool Initialize(int height, int width)
    {
        if (height == 0 || width == 0) return false;
        
        Maze = new List<List<MazeVertex>>();
        
        for (var i = 0; i < height; i++)
        {
            Maze.Add(new List<MazeVertex>());
            for (var j = 0; j < width; j++)
            {
                Maze[i].Add(new MazeVertex(false, false, false, false));
            }
        }
        return true;
    }
    
    
    /*
     * Função temporáriamente apenas converte cada celula para uma representação 3x3 para
     * uma visualização rápida e prática no console.
     */
    public void ConvertMazeToResponseType()
    {
        if (Maze is null) return;

        int rows = Maze.Count;
        int cols = Maze[0].Count;

        for (int y = 0; y < rows; y++)
        {
            // Imprimir a parede superior de cada célula na linha atual
            for (int x = 0; x < cols; x++)
            {
                Console.Write(Maze[y][x].UpEdge ? "# #" : "###");
            }
            Console.WriteLine("#"); // Fechar a linha com a borda direita do último vértice

            // Imprimir as laterais esquerda e direita de cada célula
            for (int x = 0; x < cols; x++)
            {
                Console.Write(Maze[y][x].LeftEdge ? " " : "#");
                Console.Write(" ");
                Console.Write(Maze[y][x].RightEdge ? " " : "#");
            }
            Console.WriteLine("#"); // Fechar a linha com a borda direita do último vértice
            
            // Imprimir a parede inferior de cada célula na linha atual
            for (int x = 0; x < cols; x++)
            {
                Console.Write(Maze[y][x].DownEdge ? "# #" : "###");
            }
            Console.WriteLine("#"); // Fechar a linha com a borda direita do último vértice
        }
    }
}