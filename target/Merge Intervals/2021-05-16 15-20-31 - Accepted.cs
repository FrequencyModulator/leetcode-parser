/*
Status: Accepted
Runtime: 240 ms
Memory: 33.8 MB
URL: http://leetcode.com/submissions/detail/494102402/
Submission DateTime: May 16, 2021 3:20:31 PM
*/
public class Solution {
    public int[][] Merge(int[][] intervals) {
      // 1. sort 
      Array.Sort(intervals, (x, y) => x[0] - y[0]);
      
      // 2. Merge intervals
      List<int[]> result = new List<int[]>();
      int begin = intervals[0][0];
      int end = intervals[0][1];      
      for(int i = 1; i <= intervals.Length; i++)
      {
        int currentBegin = -1;
        int currentEnd = - 1;
        if(i < intervals.Length)
        {
          currentBegin = intervals[i][0];
          currentEnd = intervals[i][1];
        }
        else
        {
          currentBegin = Int32.MaxValue;
          currentEnd = Int32.MaxValue;
        }
        if(end < currentBegin)
        {
          result.Add(new[]{begin, end});
          begin = currentBegin;
          end = currentEnd;
        }
        else
        {
          end = Math.Max(end, currentEnd);
        }
      }
      
      return result.ToArray();
                 
    }
}
