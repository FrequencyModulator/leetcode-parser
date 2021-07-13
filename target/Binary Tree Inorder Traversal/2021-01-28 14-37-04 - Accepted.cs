/*
Status: Accepted
Runtime: 240 ms
Memory: 30.9 MB
URL: http://leetcode.com/submissions/detail/449057637/
Submission DateTime: January 28, 2021 2:37:04 PM
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
    public IList<int> InorderTraversal(TreeNode root) {
        var result = new List<int>();
        
        Traverse(root, result);
      
        return result;
    }
    
    private static void Traverse(TreeNode node, List<int> result)
    {
      if(node == null)
        return;
      Traverse(node.left, result);
      result.Add(node.val);
      Traverse(node.right, result);
    }
}
