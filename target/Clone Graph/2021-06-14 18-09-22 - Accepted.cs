/*
Status: Accepted
Runtime: 240 ms
Memory: 32 MB
URL: http://leetcode.com/submissions/detail/507986562/
Submission DateTime: June 14, 2021 6:09:22 PM
*/
/*
// Definition for a Node.
public class Node {
    public int val;
    public IList<Node> neighbors;

    public Node() {
        val = 0;
        neighbors = new List<Node>();
    }

    public Node(int _val) {
        val = _val;
        neighbors = new List<Node>();
    }

    public Node(int _val, List<Node> _neighbors) {
        val = _val;
        neighbors = _neighbors;
    }
}
*/

public class Solution {
    public Node CloneGraph(Node node) 
    {
        return CloneGraph(node, new Dictionary<Node, Node>());
    }
  
    private Node CloneGraph(Node node, Dictionary<Node, Node> clonned)
    {
      if(node == null)
        return null;
      
      if(clonned.ContainsKey(node))
        return clonned[node];
      
      
      var cloneNode = new Node(node.val, new List<Node>());
      clonned[node] = cloneNode;
      foreach(var n in node.neighbors)
      {
        cloneNode.neighbors.Add(CloneGraph(n, clonned));
      }      
      return cloneNode;      
    }
}
