/* ************************************************
*
*               Raphael Frei - 2022
*             KRUSKAL Algorithm in C#
*
************************************************ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Node {

    public string name;
    public List<string> connections = new();

    public Node(string name) {
        this.name = name;
        connections = new();
    }

    public Node() {
        name = "";
        connections = new();
    }

    public void AddCon(string connection) {
        connections.Add(connection);
        
    }

    public int GetCon() {
        return connections.Count;
    }
    
}
