/*
Status: Accepted
Runtime: 160 ms
Memory: 29.7 MB
URL: http://leetcode.com/submissions/detail/521599197/
Submission DateTime: July 12, 2021 9:10:47 PM
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
        var pAncestors = new HashSet<Node>();
        var pp = p;
        while(pp != null)
        {
          pAncestors.Add(pp);
          pp = pp.parent;          
        }
      
        var qq = q;
        while(qq != null && !pAncestors.Contains(qq))
          qq = qq.parent;
      
        return qq;
    }
}
