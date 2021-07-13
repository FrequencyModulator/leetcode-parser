/*
Status: Accepted
Runtime: 264 ms
Memory: 36.4 MB
URL: http://leetcode.com/submissions/detail/504537878/
Submission DateTime: June 7, 2021 2:25:56 PM
*/
public class Solution {
    public int[][] IntervalIntersection(int[][] firstList, int[][] secondList) 
    {
        int i = 0;
        int j = 0;
        List<int[]> result = new List<int[]>();
        while(i < firstList.Length && j < secondList.Length)
        {
          int low = Math.Max(firstList[i][0], secondList[j][0]); // most right begin
          int hi = Math.Min(firstList[i][1], secondList[j][1]); // most left end
          
          if(low <= hi)
            result.Add(new[]{low, hi});
          
          // Remove the interval with the smallest endpoint
          if(firstList[i][1] < secondList[j][1])
            i++;
          else 
            j++;
        }
      
        return result.ToArray();
    }
}
