/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/521579177/
Submission DateTime: July 12, 2021 8:05:22 PM
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
    public int DiameterOfBinaryTree(TreeNode root) 
    {
      var (l, r) = Width(root, 0, 0);
      return r - l;
    }
  
    private (int, int) Width(TreeNode root, int l, int r)
    {      
      
      int ll = l;
      int rr = r;
      if(root.left != null)
      {
        (ll, rr) = Width(root.left, l - 1, r - 1);
        l = Math.Min(l, ll);    
      }
      if(root.right != null)
      {
       (ll, rr) = Width(root.right, l + 1, r + 1);
        r = Math.Max(r, rr);
      }
      return (l, r);
    }
  
  
}
