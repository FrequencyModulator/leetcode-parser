/*
Status: Runtime Error
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/448199461/
Submission DateTime: January 26, 2021 3:34:52 PM
*/
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
public class Solution {
    public bool HasCycle(ListNode head) {
      // Fast/Slow pointer      
      ListNode slowNode = head, fastNode = head.next;                        
      while(fastNode != slowNode)
      {
        if(fastNode == null)
          return false;                        
        slowNode = slowNode?.next;
        fastNode = fastNode?.next?.next;        
      }
      
      return true;
    }    
}
