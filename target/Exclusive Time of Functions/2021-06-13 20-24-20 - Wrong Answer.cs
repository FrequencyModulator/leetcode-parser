/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/507547266/
Submission DateTime: June 13, 2021 8:24:20 PM
*/
public class Solution {
    public int[] ExclusiveTime(int n, IList<string> logs) 
    {
      int[] result = new int[n];
      var stack = new Stack<(int, int)>();
      int prevT = 0;
      foreach(var l in logs.Select(x => x.Split(":")))
      {
        if(l[1] == "start")
          stack.Push((Int32.Parse(l[0]), Int32.Parse(l[2])));
        else
        {
          var (id, startOn) = stack.Pop();
          var endOn = Int32.Parse(l[2]);
          result[id] += endOn - startOn - prevT + 1;
          prevT = endOn - 1;
        }
      }
      
      return result;
    }
}
