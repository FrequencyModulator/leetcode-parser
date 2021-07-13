/*
Status: Accepted
Runtime: 268 ms
Memory: 34.8 MB
URL: http://leetcode.com/submissions/detail/473136465/
Submission DateTime: March 27, 2021 3:34:02 PM
*/
public class Solution {
    public string[] ReorderLogFiles(string[] logs) 
    {
            
      var comparer = Comparer<(int inx, string s)>.Create(Comparer);
      var a = logs.Select((s, i) => (i,s)).ToArray();
      Array.Sort(a, comparer);
      return a.Select(s => s.s).ToArray();
    }
  
    private int Comparer((int inx, string s) log1, (int inx, string s) log2)
    {
      var x = log1.s.Split(' ');
      var y = log2.s.Split(' ');      
      
      if(!IsDigit(x) && !IsDigit(y)) // both are letter-logs
        return LexiComparer(x, y);
      
      if(IsDigit(x) && !IsDigit(y)) 
        return 1;
      else if(!IsDigit(x) && IsDigit(y)) 
        return -1;
      else 
        return log1.inx - log2.inx; 
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
