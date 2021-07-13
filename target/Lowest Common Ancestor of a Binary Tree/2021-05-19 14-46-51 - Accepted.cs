/*
Status: Accepted
Runtime: 104 ms
Memory: 29.3 MB
URL: http://leetcode.com/submissions/detail/495457637/
Submission DateTime: May 19, 2021 2:46:51 PM
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
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) 
    {      
      TreeNode ans = null;
      Dfs(root, p.val, q.val, ref ans);
      return ans;
    }
  
    private bool Dfs(TreeNode root, int p, int q, ref TreeNode ans)
    {
      if(ans != null || root == null)
        return false;
      
      int left = 0, right = 0, mid = 0;
      if(root.val == p || root.val == q)
        mid = 1;
      left = Dfs(root.left, p, q, ref ans) ? 1 : 0;
      right = Dfs(root.right, p, q, ref ans) ? 1 : 0;
      
      
      if((left + right + mid) >= 2)
        ans = root;
      
      return (left + right + mid) > 0;
    }
      
}
