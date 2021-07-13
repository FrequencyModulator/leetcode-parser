/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/473114838/
Submission DateTime: March 27, 2021 2:28:24 PM
*/
public class Solution {
    public string[] ReorderLogFiles(string[] logs) 
    {
      var comparer = Comparer<string>.Create(Comparer);
      Array.Sort(logs, comparer);
      return logs;
    }
  
    private int Comparer(string log1, string log2)
    {
      var x = log1.Split(' ');
      var y = log2.Split(' ');
      
      if(!IsDigit(x) && !IsDigit(y)) // both are letter-logs
        return LexiComparer(x, y);
      else if(IsDigit(x) && IsDigit(y)) // both are digit-logs
        return 1; // 0 - maintaine original order
      else if(IsDigit(x)) 
        return 1; // digit-log is always large then letter
      else
        return -1;        
    }
    
    private bool IsDigit(string[] log) => Char.IsNumber(log[1][0]);      
  
    private int LexiComparer(string[] x, string[] y)
    {
      int l = Math.Min(x.Length, y.Length);
      for(int i = 1; i < l; i++)
      {
        var r = StringComparer.OrdinalIgnoreCase.Compare(x[i], y[i]);
        if(r != 0)
          return r;
      }
      if(x.Length == y.Length)
        return StringComparer.OrdinalIgnoreCase.Compare(x[0], y[0]);
      else if(x.Length < y.Length)
        return -1;
      else
        return 1;
    }
}
