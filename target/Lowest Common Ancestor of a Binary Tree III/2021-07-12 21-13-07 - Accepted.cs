/*
Status: Accepted
Runtime: 164 ms
Memory: 29.8 MB
URL: http://leetcode.com/submissions/detail/521600034/
Submission DateTime: July 12, 2021 9:13:07 PM
*/
/*
// Definition for a Node.
public class Node {
    public int val;
    public Node left;
    public Node right;
    public Node parent;
}
*/

public class Solution {
    public Node LowestCommonAncestor(Node p, Node q) 
    {
        // all p +  p's ancestors from p to the root
        var pAncestors = new HashSet<Node>();
        var pp = p;
        while(pp != null)
        {
          pAncestors.Add(pp);
          pp = pp.parent;          
        }
      
        // traverse from q to root 
        // stop when find pp ancestor
        var qq = q;
        while(!pAncestors.Contains(qq))
          qq = qq.parent;
      
        return qq;
    }
}
