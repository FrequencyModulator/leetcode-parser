/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/432777153/
Submission DateTime: December 20, 2020 7:37:22 PM
*/
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution {
    public bool IsValidBST(TreeNode root) {
      return Traverse(root, Int32.MaxValue, Int32.MinValue);
    }
  
    public bool Traverse(TreeNode node, int min, int max)
    {
      if(node == null)
        return true;      
      
      if(node.val >= min)
          return false;                      
      
      if(node.val <= max)      
          return false;                      
      
      if(!Traverse(node.left, node.val, max))
          return false;      
      if(!Traverse(node.right, min, node.val))
          return false;
                      
      return true;
    }
  
    
}
