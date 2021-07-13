/*
Status: Accepted
Runtime: 396 ms
Memory: 51.8 MB
URL: http://leetcode.com/submissions/detail/520974391/
Submission DateTime: July 11, 2021 4:42:18 PM
*/
public class Solution {
    public int[] FindBuildings(int[] heights) 
    {
      int i = heights.Length - 1;
      int max = heights[i] - 1;
      var res = new Stack<int>();
      while(i >= 0)
      {
        if(heights[i] > max)
        {
          res.Push(i);
          max = heights[i];
        }
        i--;
      }
      return res.ToArray();
    }
}
