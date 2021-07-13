/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/377497437/
Submission DateTime: August 7, 2020 12:23:39 PM
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
    public IList<IList<int>> VerticalTraversal(TreeNode root) 
    {            
        var traversed = new List<(TreeNode TreeNode, int order)>();
        DFS((root, 0), traversed);
        var res = traversed
            .GroupBy(o => o.order)
            .Select(g => (vals: (IList<int>)g.Select(i => i.TreeNode.val).ToList(), order: g.Key))
            .OrderBy(g => g.order)
            .Select(g => g.vals)
            .ToList();
        return (IList<IList<int>>)res;
    }

    private static void DFS((TreeNode node, int order) orderNode, List<(TreeNode TreeNode, int order)> traversed)
    {
        if(orderNode.node != null)
        {
            traversed.Add(orderNode);
            DFS((orderNode.node.left, orderNode.order - 1), traversed);
            DFS((orderNode.node.right, orderNode.order + 1), traversed);
        }
    }
}
