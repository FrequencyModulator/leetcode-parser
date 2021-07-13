/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/364408453/
Submission DateTime: July 9, 2020 3:49:15 PM
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
        int maxWidth = 0;
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while(queue.Count > 0)
        {                                
            List<TreeNode> levelNodes = new List<TreeNode>();
            while(queue.Count > 0)
            {
                levelNodes.Add(queue.Dequeue());
            }
            maxWidth = Math.Max(maxWidth, levelNodes.Count);

            foreach(var node in levelNodes.Where(n => n != null))    
            {                    
                if (node.left != null)
                    queue.Enqueue(node.left);                    
                if (node.right != null)
                {
                    if (node.left == null)
                        queue.Enqueue(null);
                    queue.Enqueue(node.right);
                }
            }
        }

        return maxWidth;
    }  
}
