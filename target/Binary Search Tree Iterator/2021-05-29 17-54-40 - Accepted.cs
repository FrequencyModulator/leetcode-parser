/*
Status: Accepted
Runtime: 144 ms
Memory: 38 MB
URL: http://leetcode.com/submissions/detail/500066483/
Submission DateTime: May 29, 2021 5:54:40 PM
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
public class BSTIterator {

    private Stack<TreeNode> stack = new Stack<TreeNode>();
  
    public BSTIterator(TreeNode root) 
    {
        LeftMostInorderTravers(root);
    }
  
    private void LeftMostInorderTravers(TreeNode root)
    {
      if(root != null)
      {
        stack.Push(root);        
        LeftMostInorderTravers(root.left);
      }
    }
    
    
    public int Next() {
      var leftMostNode = stack.Pop();
      LeftMostInorderTravers(leftMostNode.right);
      return leftMostNode.val;
    }
    
    public bool HasNext() => stack.Count > 0;
}

/**
 * Your BSTIterator object will be instantiated and called as such:
 * BSTIterator obj = new BSTIterator(root);
 * int param_1 = obj.Next();
 * bool param_2 = obj.HasNext();
 */
