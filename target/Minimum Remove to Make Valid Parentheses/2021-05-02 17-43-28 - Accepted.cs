/*
Status: Accepted
Runtime: 96 ms
Memory: 31.8 MB
URL: http://leetcode.com/submissions/detail/488040223/
Submission DateTime: May 2, 2021 5:43:28 PM
*/
public class Solution {
    public string MinRemoveToMakeValid(string s) {
        var stack = new Stack<int>(); // strore indexes of '('
        var ignoreCharIndexes = new HashSet<int>();        
        for (int i = 0; i < s.Length; i++)
        {          
          if(s[i] == '(')
            stack.Push(i);            
          else if(s[i] == ')')
          {
            if(stack.Count > 0)
              stack.Pop();              
            else
              ignoreCharIndexes.Add(i);
          }
                                  
        }
        while(stack.Count > 0)
        {
          ignoreCharIndexes.Add(stack.Pop());
        }
        if(ignoreCharIndexes.Count > 0) 
        {     
          var result = new StringBuilder();
          for (int i = 0; i < s.Length; i++)
          {
            if(!ignoreCharIndexes.Contains(i))
              result.Append(s[i]);
          }
          return result.ToString();  
        }
        else
          return s;
        
    }
}
