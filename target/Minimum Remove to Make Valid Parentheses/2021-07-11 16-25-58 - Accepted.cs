/*
Status: Accepted
Runtime: 176 ms
Memory: 34.5 MB
URL: http://leetcode.com/submissions/detail/520969261/
Submission DateTime: July 11, 2021 4:25:58 PM
*/
public class Solution {
    public string MinRemoveToMakeValid(string s) 
    {
      // stack will store positions of all unpared brackets
      var bracketPos = new Stack<int>();      
      
      for(int p = 0; p < s.Length; p++)
      {
        if(s[p] == '(')
          bracketPos.Push(p);
        else if(s[p] == ')')
        {
          if(bracketPos.Count > 0)
          {
            if(s[bracketPos.Peek()] == '(')
              bracketPos.Pop();
            else
              bracketPos.Push(p);
          }
          else
            bracketPos.Push(p);            
        }
      }
      
      if(bracketPos.Count == 0)
        return s;
      
      var result = new Stack<char>();
      for(int p = s.Length - 1; p >= 0; p--)
      {
        if(bracketPos.Count > 0 && bracketPos.Peek() == p)        
          bracketPos.Pop();
        else
          result.Push(s[p]);
      }
      
      return String.Concat(result);
      
    }
}
