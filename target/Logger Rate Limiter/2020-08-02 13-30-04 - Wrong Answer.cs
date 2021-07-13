/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/375025029/
Submission DateTime: August 2, 2020 1:30:04 PM
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
        if(!messages.ContainsKey(message))
            messages.Add(message, timestamp);
        var lastTs = messages[message];
        messages[message] = timestamp;
        return lastTs < timestamp;
    }
}

/**
 * Your Logger object will be instantiated and called as such:
 * Logger obj = new Logger();
 * bool param_1 = obj.ShouldPrintMessage(timestamp,message);
 */
