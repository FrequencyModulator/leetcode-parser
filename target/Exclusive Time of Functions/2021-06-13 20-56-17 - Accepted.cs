/*
Status: Accepted
Runtime: 264 ms
Memory: 33.6 MB
URL: http://leetcode.com/submissions/detail/507555809/
Submission DateTime: June 13, 2021 8:56:17 PM
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
          var (id, startT) = stack.Pop();
          var endT = Int32.Parse(l[2]);
          int eT = endT - startT + 1;
          result[id] += eT;
          
          if(stack.Count > 0)
          {            
            var (prevId, prevStartT) = stack.Peek();            
            result[prevId] -= eT;
                                                
          }
        }
      }
      
      return result;
    }
}
