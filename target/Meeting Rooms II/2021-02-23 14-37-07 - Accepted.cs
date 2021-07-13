/*
Status: Accepted
Runtime: 112 ms
Memory: 27.7 MB
URL: http://leetcode.com/submissions/detail/459818010/
Submission DateTime: February 23, 2021 2:37:07 PM
*/
public class Solution {
    public int MinMeetingRooms(int[][] intervals) {
      var starts =  intervals.Select(i => i[0]).ToArray();
      Array.Sort(starts); // in-place sort - better memory complexity compare with linq .OrderBy()
      var ends = intervals.Select(i => i[1]).ToArray();   
      Array.Sort(ends);
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
