/*
Status: Accepted
Runtime: 300 ms
Memory: 45.1 MB
URL: http://leetcode.com/submissions/detail/519945548/
Submission DateTime: July 9, 2021 3:18:40 PM
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
    public int RangeSumBST(TreeNode root, int low, int high) 
    {
      return Dfs(root, low, high);
    }
  
    public int Dfs(TreeNode root, int low, int high)
    {
      if(root == null)
        return 0;
      
      var sum = 0;
      if(root.val >= low && root.val <= high)
        sum = root.val;
      
      return sum + Dfs(root.left, low, high) + Dfs(root.right, low, high);
    }
}
