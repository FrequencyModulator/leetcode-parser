/*
Status: Accepted
Runtime: 100 ms
Memory: 25.1 MB
URL: http://leetcode.com/submissions/detail/364440810/
Submission DateTime: July 9, 2020 5:34:46 PM
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
            int maxWidth = 1;
            var queue = new Queue<(TreeNode node, int rank)>();
            queue.Enqueue((root, 0));

            while(queue.Count > 0)
            {                                
                var levelNodes = new List<(TreeNode node, int rank)>();
                while(queue.Count > 0)
                {                    
                    levelNodes.Add(queue.Dequeue());
                }                
                                
                if(levelNodes.Count > 1)
                {
                    int minLevelRank = levelNodes.First().rank;
                    int maxLevelRank = levelNodes.Last().rank;
                    maxWidth = Math.Max(maxWidth, maxLevelRank - minLevelRank + 1);
                }
                foreach(var n in levelNodes)    
                {                                                            
                    if (n.node.left != null)
                        queue.Enqueue((n.node.left, n.rank * 2 + 1));                    
                    if (n.node.right != null)
                        queue.Enqueue((n.node.right, n.rank * 2 + 2));                                        
                }
            }

            return maxWidth;
        }  
}
