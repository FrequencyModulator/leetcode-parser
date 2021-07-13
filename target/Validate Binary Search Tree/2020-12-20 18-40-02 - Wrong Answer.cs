/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/432762682/
Submission DateTime: December 20, 2020 6:40:02 PM
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
      if(root.left == null)
        return true;      
      if(root.left.val > root.val)
        return false;
      if(!IsValidBST(root.left))
        return false;
      
      if(root.right == null)
        return true;      
      if(root.right.val < root.val)
        return false;
      if(!IsValidBST(root.right))
        return false;
      
      return true;
    }
  
    
}
