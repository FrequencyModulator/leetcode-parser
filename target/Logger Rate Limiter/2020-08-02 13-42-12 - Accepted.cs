/*
Status: Accepted
Runtime: 260 ms
Memory: 43.5 MB
URL: http://leetcode.com/submissions/detail/375029712/
Submission DateTime: August 2, 2020 1:42:12 PM
*/
public class Logger {

    private readonly Dictionary<string, int> messages = new Dictionary<string, int>();
    /** Initialize your data structure here. */
    public Logger() {
        
    }
    
    /** Returns true if the message should be printed in the given timestamp, otherwise returns false.
        If this method returns false, the message will not be printed.
        The timestamp is in seconds granularity. */
    public bool ShouldPrintMessage(int timestamp, string message) 
    {
        if (!messages.ContainsKey(message))
            {
                messages.Add(message, timestamp);
                return true;
            }
            bool res = (timestamp - messages[message]) >= 10;
            if (res) messages[message] = timestamp;
            return res;
    }
}

/**
 * Your Logger object will be instantiated and called as such:
 * Logger obj = new Logger();
 * bool param_1 = obj.ShouldPrintMessage(timestamp,message);
 */
