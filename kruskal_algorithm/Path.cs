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

class Path{
    
    public string from;
    public string to;
    public int count;

    public Path(string from, string to, int count) {
        this.from = from;
        this.to = to;
        this.count = count;

    }

    public void ClearPath() {
        from = "";
        to = "";
        count = 0;
    }


}

