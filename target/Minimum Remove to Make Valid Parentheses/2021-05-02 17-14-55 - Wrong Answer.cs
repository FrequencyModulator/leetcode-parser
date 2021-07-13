/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/488031433/
Submission DateTime: May 2, 2021 5:14:55 PM
*/
public class Solution {
    public string MinRemoveToMakeValid(string s) {
        var stack = new Stack<int>(); // strore indexes of '('
        var result = new StringBuilder();        
        for (int i = 0; i < s.Length; i++)
        {
          if(stack.Count == 0 && s[i] != '(' && s[i] != ')')
            result.Append(s[i]);
          else
          {
            if(s[i] == '(')
              stack.Push(i);          
            else if(s[i] == ')')
            {
              if(stack.Count > 0)
              {              
                var b = stack.Pop(); // get index of '('
                if(stack.Count == 0) 
                  result.Append(s.Substring(b, i - b + 1)); // get substring from first '(' to the last '('
              }
            }
          }
        }
        if(stack.Count > 0) // if '(' are still in stack, then get substring from the next symbol to the end of string
        {              
          var b = stack.Pop(); // get index of '('          
          result.Append(s.Substring(b + 1, s.Length - b - 1)); // get substring from first '(' to the last '('
        }
      
        return result.ToString();
    }
}
