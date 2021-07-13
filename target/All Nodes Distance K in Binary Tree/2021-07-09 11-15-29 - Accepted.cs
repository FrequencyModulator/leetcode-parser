/*
Status: Accepted
Runtime: 244 ms
Memory: 31.3 MB
URL: http://leetcode.com/submissions/detail/519848911/
Submission DateTime: July 9, 2021 11:15:29 AM
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
    public IList<int> DistanceK(TreeNode root, TreeNode target, int k) 
    {
        // 1. dfs from root to target and remeber distance from each ONLY TRAVERSED nodes to targed
        Dictionary<TreeNode, int> distances = new Dictionary<TreeNode, int>();
        FindDistances(root, target, distances);      
        // 2, dfs from root: if node has distance (see 1) 
        List<int> result = new List<int>();
        Dfs(root, 0, distances, k, result);
        return result;
    }
  
    private int FindDistances(TreeNode root, TreeNode target, Dictionary<TreeNode, int> distances)
    {
      if(root == null)
        return -1;
      if(root == target)
      {
        distances.Add(target, 0);
        return 0;
      }
      
      var d = FindDistances(root.left, target, distances);
      if(d >= 0)
      {
        distances.Add(root, d + 1);
        return d + 1;
      }
      
      d = FindDistances(root.right, target, distances);
      if(d >= 0)
      {
        distances.Add(root, d + 1);
        return d + 1;
      }
      
      return -1;
    }
  
    private void Dfs(TreeNode root, int dist, Dictionary<TreeNode, int> distances, int k, List<int> result)
    {
      if(root == null)
        return;
      if(distances.ContainsKey(root))
        dist = distances[root];
      
      if(dist == k)
        result.Add(root.val);
        
      Dfs(root.left, dist + 1, distances, k, result);
      Dfs(root.right, dist + 1, distances, k, result);
    }
}
