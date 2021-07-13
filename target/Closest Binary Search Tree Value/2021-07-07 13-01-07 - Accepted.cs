/*
Status: Accepted
Runtime: 92 ms
Memory: 26.6 MB
URL: http://leetcode.com/submissions/detail/518860513/
Submission DateTime: July 7, 2021 1:01:07 PM
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
    public int ClosestValue(TreeNode root, double target) 
    {
        int closest = root.val;
        while(root != null)
        {
          if(Math.Abs(closest - target) > Math.Abs(root.val - target))
            closest = root.val;
          root = target < root.val ? root.left : root.right;
        }
      
        return closest;
    }
}
