/*
Status: Runtime Error
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/495390850/
Submission DateTime: May 19, 2021 11:52:04 AM
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
    public IList<int> RightSideView(TreeNode root) {
      var result = new List<int>();
      // BFS      
      var queue = new Queue<TreeNode>();
      TreeNode curr = root;
      TreeNode prev;
      queue.Enqueue(curr);
      queue.Enqueue(null); // null is Sentinel
      while(queue.Count > 0)
      {
        prev = curr;
        
        curr = queue.Dequeue();
        while(curr != null) // do until current is Sentinel
        {
          if(curr.left != null)
            queue.Enqueue(curr.left);
          if(curr.right != null)
            queue.Enqueue(curr.right);         
          prev = curr;
          curr = queue.Dequeue();
        }
        
        // he is prev is a most right on current level
        result.Add(prev.val);
        if(queue.Count > 0)
          queue.Enqueue(null);
      }
      return result;
    }
}
