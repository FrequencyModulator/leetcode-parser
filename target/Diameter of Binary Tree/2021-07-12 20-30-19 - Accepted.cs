/*
Status: Accepted
Runtime: 88 ms
Memory: 26.4 MB
URL: http://leetcode.com/submissions/detail/521586639/
Submission DateTime: July 12, 2021 8:30:19 PM
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
    private int d = 0;
    public int DiameterOfBinaryTree(TreeNode root) 
    {
        LongestPath(root);
        return d;
    }
  
    public int LongestPath(TreeNode root)
    {
      if(root == null)
        return 0;
      
      var leftLongestPath = LongestPath(root.left);
      var rightLongestPath = LongestPath(root.right);
      
      d = Math.Max(d, leftLongestPath + rightLongestPath);
      
      return Math.Max(leftLongestPath, rightLongestPath) + 1;      
    }
}
