/*
Status: Accepted
Runtime: 152 ms
Memory: 25.2 MB
URL: http://leetcode.com/submissions/detail/500982671/
Submission DateTime: May 31, 2021 2:51:44 PM
*/
/*
// Definition for a Node.
public class Node {
    public int val;
    public Node left;
    public Node right;

    public Node() {}

    public Node(int _val) {
        val = _val;
        left = null;
        right = null;
    }

    public Node(int _val,Node _left,Node _right) {
        val = _val;
        left = _left;
        right = _right;
    }
}
*/

public class Solution 
{
    Node first;
    Node last;  
  
    public Node TreeToDoublyList(Node root) {
      if (root == null) 
        return null;
      
      Convert(root);
      last.right = first;
      first.left = last;
      
      return first;
    }
  
    private void Convert(Node curr)
    {
      if(curr == null)
        return;
      
      Convert(curr.left);
      if(last != null)
      {
        last.right = curr;
        curr.left = last;
      }
      else
        first = curr;
      last = curr;
      
      Convert(curr.right);
    }
}
