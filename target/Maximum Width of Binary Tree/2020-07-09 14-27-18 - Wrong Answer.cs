/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/364378945/
Submission DateTime: July 9, 2020 2:27:18 PM
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
    public int WidthOfBinaryTree(TreeNode root)
        {
            int minLeft = 0;
            int maxRight = 0;

            //DFS
            DFS(root, 0, ref minLeft, ref maxRight);

            return maxRight - minLeft;
        }   

        private void DFS(TreeNode node, int point, ref int minLeft, ref int maxRight)
        {
            minLeft = Math.Min(minLeft, point);
            maxRight = Math.Max(maxRight, point);

            if(node.left != null)            
                DFS(node.left, point - 1, ref minLeft, ref maxRight);
            
            if(node.right != null)
                DFS(node.right, point + 1, ref minLeft, ref maxRight);
        }
}
