/*
Status: Accepted
Runtime: 108 ms
Memory: 27.9 MB
URL: http://leetcode.com/submissions/detail/459816533/
Submission DateTime: February 23, 2021 2:32:40 PM
*/
public class Solution {
    public int MinMeetingRooms(int[][] intervals) {
      var starts = intervals.Select(i => i[0]).OrderBy(s => s).ToArray();
      var ends = intervals.Select(i => i[1]).OrderBy(e => e).ToArray();   
      int rooms = 0;
      int endIdx = 0;
      
      for(int startIdx = 0; startIdx < intervals.Length; startIdx++)
      {
        rooms++;
        if(starts[startIdx] >= ends[endIdx])
        {
          rooms--;
          endIdx++;
        }                        
      }
                  
      return rooms;
    }
}
