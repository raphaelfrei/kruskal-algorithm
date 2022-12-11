/* ************************************************
*                                                 *
*               Raphael Frei - 2022               *
*             KRUSKAL Algorithm in C#             *
*                                                 *
************************************************ */
/*
 *      Versions:
 *          > 09/12/22: v1.0 - Document Creation
 *          > 10/12/22: v1.1 - Node + 3-Cycle Break (Node discontinued)
 *          > 10/12/22: v1.2 - Cycle break working
 *          
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

    Grafos não orientados

    Determinar uma árvore geradora em que a soma dos valores associados às arestas seja mínimo;
        l Os valores devem ser nulos ou positivos


    Feito com Base no Grafo:

          5    (1)
      B ----- D _)
    7/|    __/| \4
    A |3__/6 2|  F
    8\|/  3   | /2
      C ----- E
          6

*/

namespace kruskal_algorithm {

    class Program {

        public static List<Path> graphs = new();
        public static List<Path> f_graph = new();

        public static int path_quantity = 0;

        public static float min_cost = 0;

        public static void Main() {


            //// --------- 1º Passo ---------
            // Receber valores

            Console.WriteLine($"Insira a quantidade de caminhos: (Entre 5 e 15)");
            
            try {
                path_quantity = Convert.ToInt32(Console.ReadLine());
                
            } catch (Exception) {
                Console.Clear();
                
                Console.WriteLine($"Insira um valor válido!");

                Thread.Sleep(2000);
                RestartProgram();

            }
            
            // Não permitir menor de 5 ou maior que 15
            if (path_quantity < 5)
                path_quantity = 5;
            
            if (path_quantity > 15)
                path_quantity = 15;

            //// --------- 2º Passo ---------
            // Criar e organizar um grafo parcial; Ordenar em ordem crescente (do caminho)

            /* USUARIO */
            for (int i = 0; i < path_quantity; i++) {

                Console.WriteLine($"Insira o FROM {i + 1}: (Apenas um caracter)");
                string from = Console.ReadLine()[..1];

                Console.WriteLine($"Insira o TO {i + 1}: (Apenas um caracter)");
                string to = Console.ReadLine()[..1];

                Console.WriteLine($"Insira o COUNT {i + 1}: (Apenas número)");
                int count = Convert.ToInt32(Console.ReadLine());

                graphs.Add(new Path(from, to, count));

            }
            

            /* MODELO 1
            graphs.Add(new Path("a", "b", 7));
            graphs.Add(new Path("a", "c", 8));
            graphs.Add(new Path("b", "c", 3));
            graphs.Add(new Path("b", "d", 5));
            graphs.Add(new Path("c", "d", 6));
            graphs.Add(new Path("c", "e", 3));
            graphs.Add(new Path("d", "d", 1));
            graphs.Add(new Path("d", "e", 2));
            graphs.Add(new Path("d", "f", 4));
            graphs.Add(new Path("e", "c", 6));
            graphs.Add(new Path("e", "f", 2));
            */

            /* MODELO 2 
            graphs.Add(new Path("1", "2", 28));
            graphs.Add(new Path("6", "1", 10));
            graphs.Add(new Path("2", "3", 16));
            graphs.Add(new Path("2", "7", 14));
            graphs.Add(new Path("3", "4", 12));
            graphs.Add(new Path("4", "5", 22));
            graphs.Add(new Path("4", "7", 18));
            graphs.Add(new Path("5", "6", 25));
            graphs.Add(new Path("5", "7", 24));
            */

            RemoveCicles(graphs);
            RemoveParalels(graphs);

            //// --------- 3º Passo ---------
            // Criar um novo grafo com o menor valor

            int verts = 0;
            List<Path> temp = new();

            temp = graphs.DistinctBy(x => x.from).ToList();
            verts = temp.Count;

            graphs.Sort((x, y) => x.count.CompareTo(y.count));
            DisplayList(graphs);
            //graphs.OrderBy(x => x.from).ToList();

            // Cria um novo grafo
            for (int i = 0; i < graphs.Count; i++) {

                int occur;

                if (f_graph.Count == 0) {
                    f_graph.Add(graphs[i]);
                    continue;

                } else {

                    occur = 0;
                    
                    for (int j = 0; j < graphs.Count; j++) {

                        Console.WriteLine($"i: {i} | j: {j} | verts: {verts} | occur: {occur} |");

                        //if (graphs[i].to == graphs[j].to || graphs[i].from == graphs[j].from)
                        if ((graphs[i].to == graphs[j].to && graphs[i].from != graphs[j].from) || (graphs[i].to != graphs[j].to && graphs[i].from == graphs[j].from))
                            occur++;

                        if (occur > 0) {
                            Console.WriteLine($"Removed: {graphs[j].from}, {graphs[j].to}");

                            graphs.RemoveAt(j);

                            occur--;
                            j--;
                            i--;
                        }

                    }

                    f_graph.Add(graphs[i]);
                    //verts--;

                }
            }

            RemoveRepeated(f_graph);

            Console.WriteLine("-----------------------------------");

            // Minimo custo
            for (int i = 0; i < f_graph.Count; i++)
                min_cost += f_graph[i].count;

            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("The new graph node is:");
            DisplayList(f_graph.OrderBy(x => x.count).ToList());
            Console.WriteLine($"Minimal cost: {min_cost}");

        }

        // Remover Ciclos
        public static void RemoveCicles(List<Path> list) {

            for (int i = 0; i < list.Count; i++) {

                if (list[i].from == list[i].to) {
                    list.RemoveAt(i);
                    i--;
                }

            }
            
        }

        // Remover repetidos
        public static void RemoveRepeated(List<Path> list) {
            
            for(int i = 0; i < list.Count; i++) {

                for (int j = i + 1; j < list.Count; j++)
                    if (list[i].from == list[j].from && list[i].to == list[j].to) {
                        list.RemoveAt(j);
                        j--;
                    }

            }

        }

        // Remover paralelos
        public static void RemoveParalels(List<Path> list) {
            
            for (int i = 0; i < list.Count; i++) {
                for (int j = 0; j < list.Count; j++) {

                    if (i != j) {

                        if (list[i].from == list[j].to && list[i].to == list[j].from) {
                            list.RemoveAt(j);
                            j--;
                        }

                        if (list[i].from == list[j].from && list[i].to == list[j].to) {

                            if (list[i].count < list[j].count)
                                list.RemoveAt(j);
                             else if (list[i].count > list[j].count)
                                list.RemoveAt(i);

                            j--;
                        }
                    } 

                }
            }

        }

        // Limpa a lista
        public static void ClearPaths() {

            graphs.Clear();
            f_graph.Clear();

        }
        
        // Deleta todos os caminhos e reseta o programa
        public static void RestartProgram() {

            ClearPaths();
            Console.Clear();
            Main();
            
        }

        // Exibe a lista de grafos
        public static void DisplayList(List<Path> list) {

            foreach (Path path in list)
                Console.WriteLine($"FROM: {path.from} | TO: {path.to} | COUNT: {path.count}");

        }

    }
}