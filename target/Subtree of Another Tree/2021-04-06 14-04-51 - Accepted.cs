/*
Status: Accepted
Runtime: 108 ms
Memory: 28.1 MB
URL: http://leetcode.com/submissions/detail/477340119/
Submission DateTime: April 6, 2021 2:04:51 PM
*/
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    public bool IsSubtree(TreeNode s, TreeNode t) {              
      return s != null && (IsEqual(s,t) || IsSubtree(s.left, t) || IsSubtree(s.right, t));        
    }
    
    private bool IsEqual(TreeNode x, TreeNode y)
    {
      if(x == null && y == null)
        return true;
      else if(x == null || y == null)
        return false;
      return x.val == y.val && IsEqual(x.left, y.left) && IsEqual(x.right, y.right);
    }
}
