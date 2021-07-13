/*
Status: Accepted
Runtime: 268 ms
Memory: 34.9 MB
URL: http://leetcode.com/submissions/detail/473139841/
Submission DateTime: March 27, 2021 3:44:30 PM
*/
public class Solution {
    public string[] ReorderLogFiles(string[] logs) 
    {
            
      var comparer = Comparer<(int inx, string s)>.Create(Comparer);
      return logs.Select((s, i) => (i,s)).OrderBy(p => p, comparer).Select(p => p.s).ToArray();            
    }
  
    private int Comparer((int inx, string s) log1, (int inx, string s) log2)
    {
      var x = log1.s.Split(' ');
      var y = log2.s.Split(' ');    
      
      var isDigit1 = Char.IsNumber(x[1][0]);
      var isDigit2 = Char.IsNumber(y[1][0]);
      
      if(!isDigit1 && !isDigit2) // both are letter-logs
        return LexiComparer(x, y);
      
      if(isDigit1 && !isDigit2) 
        return 1;
      else if(!isDigit1 && isDigit2) 
        return -1;
      else 
        return log1.inx - log2.inx; 
    }
       
  
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
