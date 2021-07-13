/*
Status: Accepted
Runtime: 260 ms
Memory: 31 MB
URL: http://leetcode.com/submissions/detail/377538877/
Submission DateTime: August 7, 2020 2:22:28 PM
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
        var traversed = new List<(TreeNode node, int x, int y)>();
        DFS(root, 0, 0, traversed);
        var res = traversed
            .GroupBy(o => o.x)
            .Select(g => 
            (
                vals: g.OrderBy(v => v, Comparer<(TreeNode node, int x, int y)>.Create(CompareSameX)).Select(v => v.node.val), 
                x: g.Key
            ))
            .OrderBy(g => g.x)
            .Select(g => (IList<int>)g.vals.ToList())
            .ToList();
        return (IList<IList<int>>)res;
    }

    private int CompareSameX((TreeNode node, int x, int y) v1, (TreeNode node, int x, int y) v2) => v1.y != v2.y ? v1.y - v2.y : v1.node.val - v2.node.val;

    private static void DFS(TreeNode node, int x, int y, List<(TreeNode node, int x, int y)> traversed)
    {
        if(node != null)
        {
            traversed.Add((node, x, y));
            DFS(node.left, x - 1, y + 1, traversed);
            DFS(node.right, x + 1, y + 1, traversed);
        }
    }
}
